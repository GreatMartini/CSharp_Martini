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
//using Microsoft.Data.Analysis;


namespace Projet_1{
    //public class Tableaux{

    //}

    public class Banque{
        public Dictionary <uint, decimal> Tableau_comptes(){
            Dictionary <uint, decimal> comptes = new Dictionary<uint, decimal>();
            if (File.Exists("Comtpes.csv")){
                using (StreamReader r = new StreamReader("Comptes.csv")){
                    while(!r.EndOfStream){
                        var line = r.ReadLine();
                        var values = line.Split(';');
                        bool code_correct = uint.TryParse(values[0], out uint code);
                        bool montant_correct = decimal.TryParse(values[1], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal montant);
                        if (code_correct && montant_correct){
                            comptes.Add(code, montant);
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
                using (StreamReader r = new StreamReader("Transaction.csv")){
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
            using (StreamWriter w = new StreamWriter("Status.csv, true")){
                w.WriteLine("${code_trans};{stat}");               
            }
        }


        public void Traite_transactions(){

            // Declaration des variables pre-traitement
            List <uint> codes_transaction = new List <uint>();              // Liste de codes pour tester le fichier
            Dictionary <uint, decimal> comptes = Tableau_comptes();
            List< List <string>> transactions = Tableau_transactions();
            string statut;                                                  // Garde le statut de la transaction

            // On regarde si il y des comptes non-existants    
            for (int i = 0; i < transactions.Count(); i++){
                    List <string> ligne_transaction = transactions[i];
                    bool code_correct = uint.TryParse(ligne_transaction[0], out uint code_aux);
                    bool expediteur_correct = uint.TryParse(ligne_transaction[2], out uint expediteur);
                    bool destinataire_correct = uint.TryParse(ligne_transaction[3], out uint destinataire);
                    if (comptes.ContainsKey(expediteur) == false || comptes.ContainsKey(destinataire) == false || destinataire_correct == false || expediteur_correct == false){
                        statut = "KO";
                        if (code_correct){
                            ecrire(code_aux,statut);      
                        }
                        else{
                            continue;
                        }            
                    }
                    else{
                        continue;
                    }                   

            }

            // On commence le traitement par comptes              
            foreach (var item in comptes){
                List< decimal> historique = new List<decimal>();
                Compte compte_i = new Compte(item.Key);
            }
    /*                var line = r.ReadLine();
                    var values = line.Split(';');

                    bool code_correct = uint.TryParse(values[0], out uint code_aux);
                    bool montant_correct = decimal.TryParse(values[1], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal montant);
                    bool expediteur_correct = uint.TryParse(values[2], out uint expediteur);
                    bool destinataire_correct = uint.TryParse(values[3], out uint destinataire);

                    if (code_correct == false || montant_correct == false || expediteur_correct == false || destinataire_correct== false ){
                        statut = "KO";
                        ecrire(code_aux, statut);

                        continue;
                    }


                    if (!codes_transaction.Contains(code_aux)){
                        codes_transaction.Add(code_aux);

                        if(values[2] != values [3]){

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
        }
    }
}