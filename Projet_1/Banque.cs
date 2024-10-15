using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
//using Microsoft.Data.Analysis;


namespace Projet_1{
    //public class Tableaux{

    //}

    public class Banque{
        public Dictionary <uint, Compte> Tableau_comptes(){
            Dictionary <uint, Compte> comptes = new Dictionary<uint, Compte>();
            if (File.Exists("Comptes.csv")){
                using (StreamReader r = new StreamReader("Comptes.csv")){
                    while(!r.EndOfStream){
                        var line = r.ReadLine();
                        var values = line.Split(';');
                        bool code_correct = uint.TryParse(values[0], out uint code);
                        bool montant_correct = decimal.TryParse(values[1], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal montant);
                        if (code_correct && montant_correct){
                            Compte compte_i = new Compte(code, montant);
                            comptes.Add(code, compte_i);
                        }
                        else{
                            continue;
                        }  
                    }
                    return comptes;
                }
            }
            else{
                throw new Exception("Comptes.csv non existant");
                }

        }

        public List< List <string>> Tableau_transactions(){
            List <List<string>> transactions = new List<List<string>>();
            List <string> ligne_transaction = new List<string>();
            if (File.Exists("Transactions.csv")){
                using (StreamReader r = new StreamReader("Transactions.csv")){
                    while(!r.EndOfStream){
                        var line = r.ReadLine();
                        var values = line.Split(';');
                        ligne_transaction.Add(values[0]);
                        ligne_transaction.Add(values[1]);
                        transactions.Add(ligne_transaction);
                    }
                }
            return transactions;
            }
            else{
                throw new Exception("Transactions.csv non existant");
            }
        }

        public void ecrire(uint code_trans, string stat){                                       // Ecrit le fichier de sortie status
            using (StreamWriter w = new StreamWriter("Status.csv", true)){
                w.WriteLine($"{code_trans};{stat}");               
            }
        }

        public void Traite_transactions(){

            // Declaration des variables pre-traitement
            List <uint> codes_transaction = new List <uint>();              // Liste de codes pour tester le fichier
            Dictionary <uint, Compte> comptes = Tableau_comptes();
            List< List <string>> transactions = Tableau_transactions();
            string statut;                                                  // Garde le statut de la transaction


            // On regarde si il y des comptes non-existants    
            for (int i = 0; i < transactions.Count(); i++){
                    List <string> ligne_transaction = transactions[i];      // Lignes de transactions
                    List <uint> transactions_passees = new List<uint>();

                    bool code_correct = uint.TryParse(ligne_transaction[0], out uint code_aux);
                    bool expediteur_correct = uint.TryParse(ligne_transaction[2], out uint expediteur);
                    bool destinataire_correct = uint.TryParse(ligne_transaction[3], out uint destinataire);
                    bool montant_correct = decimal.TryParse(ligne_transaction [1], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal montant);


                    if (comptes.ContainsKey(expediteur) == false || comptes.ContainsKey(destinataire) == false || destinataire_correct == false || expediteur_correct == false || montant_correct == false){
                        statut = "KO";
                        if (code_correct){
                            ecrire(code_aux,statut);      
                        }
                        else{
                            continue;
                        }            
                    }

                    else{
                        if(!transactions_passees.Contains(code_aux)){
                            transactions_passees.Add(code_aux);
                            if (expediteur != destinataire){
                                if(expediteur == 0){
                                // Depot
                                Transaction transaction_D = new Transaction('D', code_aux, comptes[expediteur], comptes[destinataire], montant);
                                statut = transaction_D.Depot();
                                ecrire(code_aux, statut);
                                if (statut == "OK"){
                                    comptes[destinataire].solde += transaction_D.montant;                                     
                                }
                            }
                            else if (destinataire == 0){
                                // Retrait
                                Transaction transaction_R = new Transaction('R', code_aux, comptes[expediteur], comptes[destinataire], montant);
                                statut = transaction_R.Retrait();
                                ecrire(code_aux, statut);
                                if(statut == "OK"){
                                    comptes[expediteur].solde -= transaction_R.montant;
                                    comptes[expediteur].historique_transactions.Add(transaction_R.montant);
                                    comptes[expediteur].cumul_operations += transaction_R.montant;
                                }
                            }

                            else{
                                // Prélèvement et virement
                                //Transaction transaction_P = new Transaction('P', code_aux, comptes[expediteur], comptes[destinataire], montant);
                                Transaction transaction_V = new Transaction('V', code_aux, comptes[expediteur], comptes[destinataire], montant);
                                statut = transaction_V.Virement();
                                ecrire(code_aux, statut);
                                if(statut == "OK"){
                                    comptes[expediteur].solde -= transaction_V.montant;
                                    comptes[expediteur].historique_transactions.Add(transaction_V.montant);
                                    comptes[expediteur].cumul_operations += transaction_V.montant;

                                    comptes[destinataire].solde += transaction_V.montant;
                                }
                            }
                        }
                    }                  
                }                   
            }
        }
    }
}

    /* 
                            if(values[2] == "0"){
                                // Depot
                                Transaction transaction_D = new Transaction('D', code_aux, expediteur, destinataire, montant);
                                statut = transaction_D.Depot();
                                ecrire(code_aux, statut);
                                
                            }

                            else if (values[3] == "0"){
                                // Retrait
                                Transaction transaction_R = new Transaction('R', code_aux, expediteur, destinataire, montant);
                                statut = transaction_R.Depot();
                                ecrire(code_aux, statut);


                            }

                            else{
                                // Prélèvement et virement
                                Transaction transaction_P = new Transaction('P', code_aux, expediteur, destinataire, montant);

                                Transaction transaction_V = new Transaction('V', code_aux, expediteur, destinataire, montant);
                                if (transaction_V.Virement() == "OK" && transaction_P.Prelevement() == "OK"){
                                    statut = "OK";
                                }
                                else{
                                    statut = "KO";
                                }
                                ecrire(code_aux, statut);

                            }
                        }

                        else{
                            statut = "KO";
                            ecrire(code_aux, statut);
                            continue;
                        }
                    }
                    else{
                        continue;
                    }

                }                    
            }          
*/
