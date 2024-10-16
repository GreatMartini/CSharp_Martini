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
using System.Runtime.CompilerServices;
using System.Net;
using System.Linq.Expressions;



namespace Projet_2{

    public class Banque{
        /*
        public Dictionary <uint, Compte> Tableau_comptes(){
            Dictionary <uint, Compte> comptes = new Dictionary<uint, Compte>();
            if (File.Exists("Comptes.csv")){
                using (StreamReader r = new StreamReader("Comptes.csv")){
                    while(!r.EndOfStream){
                        decimal montant = 0;
                        var line = r.ReadLine();
                        var values = line.Split(';');

                        bool code_correct = uint.TryParse(values[0], out uint code);
                        bool montant_correct = decimal.TryParse(values[1], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out montant);
                        if(values[1]==""){
                            montant_correct = true;
                        }
                        bool montant_positif = false;
                        if(montant >= 0){
                            montant_positif = true;
                        }

                        if (code_correct && montant_positif && montant_correct && !comptes.ContainsKey(code)){
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
            if (File.Exists("Transactions.csv")){
                using (StreamReader r = new StreamReader("Transactions.csv")){
                    while(!r.EndOfStream){
                        List <string> ligne_transaction = new List<string>();
                        var line = r.ReadLine();
                        var values = line.Split(';');
                        ligne_transaction.Add(values[0]);
                        ligne_transaction.Add(values[1]);
                        ligne_transaction.Add(values[2]);
                        ligne_transaction.Add(values[3]);
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
        */
        public void Traite_transactions(){
            
            Charge chargement = new Charge();
            Dictionary <uint, Compte> comptes = chargement.Tableau_comptes();
            Dictionary <uint, Gestionnaire> Gestionnaires = chargement.Tableau_gestionnaires();
            List< List <string>> transactions = chargement.Tableau_transactions();


/*
            // Declaration des variables pre-traitement
            string statut;                                                  // Garde le statut de la transaction
            List <uint> transactions_passees = new List<uint>();

            // On regarde si il y des comptes non-existants    
            for (int i = 0; i < transactions.Count(); i++){
                    List <string> ligne_transaction = transactions[i];      // Lignes de transactions


                    bool code_correct = uint.TryParse(ligne_transaction[0], out uint code_aux);
                    bool expediteur_correct = uint.TryParse(ligne_transaction[2], out uint expediteur);
                    bool destinataire_correct = uint.TryParse(ligne_transaction[3], out uint destinataire);
                    bool montant_correct = decimal.TryParse(ligne_transaction [1], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal montant);


                    if (destinataire_correct == false || expediteur_correct == false || montant_correct == false){
                        statut = "KO";
                        if (code_correct){
                            ecrire(code_aux,statut);
                            continue;      
                        }
                        else{
                            continue;
                        }            
                    }
                    else{ 

                        if(transactions_passees.Contains(code_aux) == false){
                            transactions_passees.Add(code_aux);

                            if (expediteur != destinataire){
                                if(expediteur == 0){
                                    // Depot
                                    if(comptes.ContainsKey(destinataire)){
                                        Transaction transaction_D = new Transaction('D', code_aux, comptes[destinataire], comptes[destinataire], montant);
                                        statut = transaction_D.Depot();
                                        if (statut == "OK"){
                                            comptes[destinataire].solde += transaction_D.montant;                                     
                                        }
                                    }
                                    else{
                                        statut = "KO";
                                    }
                                    //ecrire(code_aux, statut);
                                }
                                else if (destinataire == 0){
                                    // Retrait
                                    if(comptes.ContainsKey(expediteur)){
                                        Transaction transaction_R = new Transaction('R', code_aux, comptes[expediteur], comptes[expediteur], montant);
                                        statut = transaction_R.Retrait();
                                        if(statut == "OK"){
                                            comptes[expediteur].solde -= transaction_R.montant;
                                            comptes[expediteur].historique_transactions.Add(transaction_R.montant);
                                            if (comptes[expediteur].historique_transactions.Count()%10 == 0){
                                                comptes[expediteur].cumul_operations = 0;
                                            }
                                            else{
                                            comptes[expediteur].cumul_operations += transaction_R.montant;
                                            }
                                        }
                                    }
                                    else{
                                        statut = "KO";
                                    }
                                    //ecrire(code_aux, statut);

                                }

                                else{

                                    if(comptes.ContainsKey(expediteur) && comptes.ContainsKey(destinataire)){
                                        Transaction transaction_V = new Transaction('V', code_aux, comptes[expediteur], comptes[destinataire], montant);
                                        statut = transaction_V.Virement();

                                        if(statut == "OK"){
                                            comptes[expediteur].solde -= transaction_V.montant;
                                            comptes[expediteur].historique_transactions.Add(transaction_V.montant);
                                            if (comptes[expediteur].historique_transactions.Count()%10 == 0){
                                                comptes[expediteur].cumul_operations = 0;
                                            }
                                            else{
                                                comptes[expediteur].cumul_operations += transaction_V.montant;
                                            }
                                            comptes[destinataire].solde += transaction_V.montant;
                                        }
                                        
                                    }
                                    else{
                                        statut = "KO";
                                    }
                                }
                            }
                            else{
                                statut = "KO";   
                            }                            
                          
                        }
                        else{
                            statut = "KO";
                        }
                        ecrire(code_aux, statut); 
                    }                  
                                   
            }*/
        }

    }
}
