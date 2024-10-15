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

namespace Projet_1{
    public class Banque{
        public Dictionary <uint, decimal> comptes = new Dictionary<uint, decimal>();            // Dictionnaire de comptes
        public void charge_comptes(){                                                           // Charge le dictionnaire de comptes
            using (StreamReader r = new StreamReader("Transaction.csv")){
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
            }
        }
        /*List< List <string>> transactions = new List<List<string>>();
        public void charge_transactions(){
            List <string> ligne_transaction = new List<string>();

        }*/
        public void ecrire(uint code_trans, string stat){                                       // Ecrit le fichier de sortie status
            using (StreamWriter w = new StreamWriter("Status")){
                w.WriteLine("${stat};{code_trans}");
                
            }
        }

        public void Traite_transactions(){
            if (File.Exists("Transactions.csv")){
                List <uint> codes_transaction = new List <uint>();              // Liste de codes pour tester le fichier
                string statut;                                                  // Garde le statut de la transaction
                using (StreamReader r = new StreamReader("Transaction.csv")){

                    while(!r.EndOfStream){

                        var line = r.ReadLine();
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
            }
            else{
                throw new Exception("Transactions.csv non existant");
            }
        }
    }
}