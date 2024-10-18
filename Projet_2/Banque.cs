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
        public List<string> Gestionnaires_contiennent_compte(Dictionary <uint, Gestionnaire> gestionnaires, uint ncompte){
            uint ngestionnaire = 0;
            bool contient_compte = false;
            List<string> ligne = new List<string>();
            foreach(var gestionnaire in gestionnaires){
               contient_compte = gestionnaire.Value.comptes.ContainsKey(ncompte);
               if (contient_compte == true){
                    ngestionnaire = gestionnaire.Key;
                    ligne.Add(contient_compte.ToString());
                    ligne.Add(ngestionnaire.ToString());
               }
            }
            return ligne;
        }
 
        // Gere les transactions: Manque d'impltémenter la gestion de dates
        public void Gere_transactions(Dictionary <uint, Gestionnaire> gestionnaires, List <List <string>> transactions){
                    // Declaration des variables pre-traitement
            List <uint> transactions_passees = new List<uint>();
            for (int i = 0; i < transactions.Count(); i++){
            List <string> ligne_transaction = transactions[i];      // Lignes de transactions



            bool code_correct = uint.TryParse(ligne_transaction[0], out uint code_aux);
            bool expediteur_correct = uint.TryParse(ligne_transaction[3], out uint expediteur);
            bool destinataire_correct = uint.TryParse(ligne_transaction[4], out uint destinataire);                          
            bool date_correcte = DateTime.TryParseExact(ligne_transaction[1],"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);                   
            bool montant_correct = decimal.TryParse(ligne_transaction [2], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out decimal montant);

            List<string> info_destinataire = Gestionnaires_contiennent_compte(gestionnaires,destinataire);
            List<string> info_expediteur =  Gestionnaires_contiennent_compte(gestionnaires,expediteur);
            if (info_expediteur.Count != 0 && info_destinataire.Count != 0){
                bool contient_expediteur_correct = bool.TryParse(info_expediteur[0], out bool contient_expediteur);
                bool contient_destinataire_correct = bool.TryParse(info_destinataire[0], out bool contient_destinataire);
                bool gestionnaire_expediteur_correct = uint.TryParse(info_expediteur[1], out uint gestionnaire_expediteur);
                bool gestionnaire_destinataire_correct = uint.TryParse(info_destinataire[1], out uint gestionnaire_destinatare);
                        

                if (destinataire_correct == false || expediteur_correct == false || montant_correct == false){
                    ligne_transaction[5] = "KO";      
                }
                else{ 

                    if(transactions_passees.Contains(code_aux) == false){
                        transactions_passees.Add(code_aux);

                        if (expediteur != destinataire){
                            if(expediteur == 0){
                                // Depot
                                if(contient_destinataire){
                                    Transaction transaction_D = new Transaction('D', code_aux, gestionnaires[gestionnaire_destinatare].comptes[destinataire], gestionnaires[gestionnaire_destinatare].comptes[destinataire], montant);
                                    ligne_transaction[5] = transaction_D.Depot();
                                    if (ligne_transaction[5] == "OK"){
                                        gestionnaires[gestionnaire_destinatare].comptes[destinataire].solde += transaction_D.montant;                                     
                                    }
                                }
                                else{
                                    ligne_transaction[5] = "KO";
                                }
                                //ecrire(code_aux, statut);
                            }
                            else if (destinataire == 0){
                                // Retrait
                                if(contient_expediteur){
                                    Transaction transaction_R = new Transaction('R', code_aux, gestionnaires[gestionnaire_expediteur].comptes[expediteur], gestionnaires[gestionnaire_expediteur].comptes[expediteur], montant);
                                    ligne_transaction[5] = transaction_R.Retrait();
                                    if(ligne_transaction[5] == "OK"){
                                        gestionnaires[gestionnaire_expediteur].comptes[expediteur].solde -= transaction_R.montant;
                                        gestionnaires[gestionnaire_expediteur].comptes[expediteur].historique_transactions.Add(transaction_R.montant);
                                        if (gestionnaires[gestionnaire_expediteur].comptes[expediteur].historique_transactions.Count()%10 == 0){
                                            gestionnaires[gestionnaire_expediteur].comptes[expediteur].cumul_operations = 0;
                                        }
                                        else{
                                        gestionnaires[gestionnaire_expediteur].comptes[expediteur].cumul_operations += transaction_R.montant;
                                        }
                                    }
                                }
                                else{
                                    ligne_transaction[5] = "KO";
                                }
                            }

                            else{

                                if(contient_expediteur && contient_destinataire){
                                    Transaction transaction_V = new Transaction('V', code_aux, gestionnaires[gestionnaire_expediteur].comptes[expediteur], gestionnaires[gestionnaire_destinatare].comptes[destinataire], montant);
                                    ligne_transaction[5] = transaction_V.Virement();

                                    if(ligne_transaction[5] == "OK"){
                                        gestionnaires[gestionnaire_expediteur].comptes[expediteur].solde -= transaction_V.montant;
                                        gestionnaires[gestionnaire_expediteur].comptes[expediteur].historique_transactions.Add(transaction_V.montant);
                                        if (gestionnaires[gestionnaire_expediteur].comptes[expediteur].historique_transactions.Count()%10 == 0){
                                            gestionnaires[gestionnaire_expediteur].comptes[expediteur].cumul_operations = 0;
                                        }
                                        else{
                                            gestionnaires[gestionnaire_expediteur].comptes[expediteur].cumul_operations += transaction_V.montant;
                                        }
                                        gestionnaires[gestionnaire_destinatare].comptes[destinataire].solde += transaction_V.montant;
                                    }
                                    
                                }
                                else{
                                    ligne_transaction[5] = "KO";
                                }
                            }
                        }
                        else{
                            ligne_transaction[5] = "KO";   
                        }                            
                        
                    }
                    else{
                        ligne_transaction[5] = "KO";
                    }
                }
            }
            else{
               ligne_transaction[5] = "KO";
               continue; 
            } 
        }    
    }

        // Fonction quir crée les comptes dans les gestionnaires et met à KO opérations mal effectuées
        public void Cree_comptes_gestionnaires(Dictionary <uint, Gestionnaire> gestionnaires, List <List <string>> comptes){
            // On itere sur chaque élément du dictionnaire des gestionnaires
            Charge chargement = new Charge();

            foreach(var element in gestionnaires){
                List <string> comptes_deja_existants = new List<string>();
                List<List <string>> comptes_aux = new List<List<string>>();                      // On crée un tableau auxiliaire qui contiendra les valeurs dans la liste ou gestionnaire de creation correspondra au dictionnaire de gestionnaires
                foreach (List <string> ligne in comptes){                                       // On itère sur chaque élement de la liste comptes
                    bool entree_correcte = uint.TryParse(ligne[3], out uint entree);            // On regarde si l'entrée peut être bien parsée
                   
                    // Si l'entrée n'est pas correcte ou qu'elle n'est pas contenue parmis les gestionnaires, l'opération est fausse
                    if(!entree_correcte || !gestionnaires.ContainsKey(entree)){                    
                        ligne[5] = "KO";
                        continue;
                    }  

                    else if (entree == element.Key && ligne[4] == ""){ //S'il s'agit d'une création est que l'entrée correspond au gestionnaire en question
                        if (comptes_deja_existants.Contains(ligne[0])){
                        ligne[5] = "KO";
                        continue;
                        }
                        else{
                            comptes_deja_existants.Add(ligne[0]);
                        } 
                        comptes_aux.Add(ligne);                        // On enregistre ligne dans le tableau auxiliaire
                    }

                    else{                                               // Tout autre cas on continue
                        continue;
                    }                                    
                }
                // On rajoute le dictionnaire de comptes créés dans chaque gestionnaires
                gestionnaires[element.Key].comptes = chargement.Tableau_comptes_crees(comptes_aux);
            }
        }

        // Fonction qui gère les opérations entre gestionnaires et met à KO les mauvaises opérations
        public void Gere_operations_comptes(Dictionary <uint, Gestionnaire> gestionnaires, List <List <string>> comptes){
            Charge chargement = new Charge();
            List <string> comptes_deja_existants = new List<string>();

            List<List <string>> comptes_aux = new List<List<string>>();                      // On crée un tableau auxiliaire qui contiendra les valeurs dans la liste ou gestionnaire de creation correspondra au dictionnaire de gestionnaires
            foreach (List <string> ligne in comptes){                                        // On itère sur chaque élement de la liste comptes
                bool code_correct = uint.TryParse(ligne[0], out uint code);
                bool entree_correcte = uint.TryParse(ligne[3], out uint entree);             // On regarde si l'entrée peut être bien parsée
                bool sortie_correcte = uint.TryParse(ligne[4], out uint sortie);             // On regarde si la sortie peut être bien parsée
                bool date_correcte = DateTime.TryParseExact(ligne[1],"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);


                // Si l'entrée n'est pas correcte ou qu'elle n'est pas contenue parmis les gestionnaires, l'opération est fausse
                if(!entree_correcte || !gestionnaires.ContainsKey(entree) || !sortie_correcte || !gestionnaires.ContainsKey(sortie) || !code_correct || !date_correcte){                    
                    ligne[5] = "KO";
                    continue;
                }
                else{
                    if(gestionnaires[entree].comptes.ContainsKey(code) && date >  gestionnaires[entree].comptes[code].date){
                        if (comptes_deja_existants.Contains(ligne[0])){
                            ligne[5] = "KO";
                            continue;
                        }
                        else{
                            comptes_deja_existants.Add(ligne[0]);
                        }  
                        gestionnaires[sortie].comptes.Add(code, gestionnaires[entree].comptes[code]);
                        gestionnaires[entree].comptes.Remove(code);
                    }
                    else{
                        ligne[5] = "KO";
                        continue;
                    }
                }
            }              
        }

        // Fonction qui cloture les comptes par gestionnaires et met a KO les mauvaises opération
        public void Cloture_comptes_gestionnaires(Dictionary <uint, Gestionnaire> gestionnaires, List <List <string>> comptes){
           Charge chargement = new Charge();

            foreach(var element in gestionnaires){
                List <string> comptes_deja_clotures = new List<string>();
                List<List <string>> comptes_aux = new List<List<string>>();                      // On crée un tableau auxiliaire qui contiendra les valeurs dans la liste ou gestionnaire de creation correspondra au dictionnaire de gestionnaires
                foreach (List <string> ligne in comptes){                                       // On itère sur chaque élement de la liste comptes
                bool code_correct = uint.TryParse(ligne[0], out uint code);
                bool entree_correcte = uint.TryParse(ligne[3], out uint entree);             // On regarde si l'entrée peut être bien parsée
                bool sortie_correcte = uint.TryParse(ligne[4], out uint sortie);             // On regarde si la sortie peut être bien parsée
                bool date_correcte = DateTime.TryParseExact(ligne[1],"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);                   
                  
                    // Si l'entrée n'est pas correcte ou qu'elle n'est pas contenue parmis les gestionnaires, l'opération est fausse
                    if(!sortie_correcte || !gestionnaires.ContainsKey(sortie)){                    
                        ligne[5] = "KO";
                        continue;
                    }  

                    else if (sortie == element.Key && ligne[3] == ""){ //S'il s'agit d'une suppression est que l'entrée correspond au gestionnaire en question
                        if (comptes_deja_clotures.Contains(ligne[0])){
                            ligne[5] = "KO";
                            continue;
                        }
                        else{
                            comptes_deja_clotures.Add(ligne[0]);
                        }  
                        if(gestionnaires[sortie].comptes.ContainsKey(code) && date >  gestionnaires[sortie].comptes[code].date){
                            gestionnaires[sortie].comptes.Remove(code);
                        }
                        else{
                            ligne[5] = "KO";
                            continue;
                        }                     // On enregistre ligne dans le tableau auxiliaire
                    }
                       
                }
            }
        }

        public void Traitement(){
            
            Charge chargement = new Charge();
            Scribe scribe = new Scribe();
            //On charge les gestionnaires dans le dictionnaire de gestionnaires
            Dictionary <uint, Gestionnaire> gestionnaires = chargement.Tableau_gestionnaires();

            // On crée des listes avec le fichier comptes et le fichier
            List <List <string>> comptes = chargement.Tableau_comptes();
            List <List <string>> transactions = chargement.Tableau_transactions();

            // On rajoute les comptes créés dans les dictionnaires des gestionnaires
            Cree_comptes_gestionnaires(gestionnaires, comptes);
            Gere_transactions(gestionnaires, transactions);


            Gere_operations_comptes (gestionnaires, comptes);
            Gere_transactions(gestionnaires, transactions);

            Cloture_comptes_gestionnaires(gestionnaires, comptes);
            Gere_transactions(gestionnaires, transactions);

            foreach(List<string> ligne in comptes){
                scribe.Ecrit_statut_comptes(ligne[0], ligne[1], ligne[2], ligne[3], ligne[4], ligne[5]);
            }                                 
            foreach(List<string> ligne in transactions){
                scribe.Ecrit_statut_transactions(ligne[0], ligne[1], ligne[2], ligne[3], ligne[4], ligne[5]);
            }                                 
        }
    }
}    
    