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

        // Fonction quir crée les comptes dans les gestionnaires et met à KO opérations mal effectuées
        public void Cree_comptes_gestionnaires(Dictionary <uint, Gestionnaire> gestionnaires, List <List <string>> comptes){
            // On itere sur chaque élément du dictionnaire des gestionnaires
            Charge chargement = new Charge();
            foreach(var element in gestionnaires){

                List<List <string>> comptes_aux = new List<List<string>>();                      // On crée un tableau auxiliaire qui contiendra les valeurs dans la liste ou gestionnaire de creation correspondra au dictionnaire de gestionnaires
                foreach (List <string> ligne in comptes){                                       // On itère sur chaque élement de la liste comptes
                    bool entree_correcte = uint.TryParse(ligne[3], out uint entree);            // On regarde si l'entrée peut être bien parsée

                    // Si l'entrée n'est pas correcte ou qu'elle n'est pas contenue parmis les gestionnaires, l'opération est fausse
                    if(!entree_correcte || !gestionnaires.ContainsKey(entree)){                    
                        ligne[5] = "KO";
                        continue;
                    }  

                    else if (entree == element.Key && ligne[4] == ""){ //S'il s'agit d'une création est que l'entrée correspond au gestionnaire en question
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

        public void Traitement(){
            
            Charge chargement = new Charge();

            //On charge les gestionnaires dans le dictionnaire de gestionnaires
            Dictionary <uint, Gestionnaire> gestionnaires = chargement.Tableau_gestionnaires();

            // On crée des listes avec le fichier comptes et le fichier
            List <List <string>> comptes = chargement.Tableau_comptes();
            List <List <string>> transactions = chargement.Tableau_transactions();

            // On rajoute les comptes créés dans les dictionnaires des gestionnaires
            Cree_comptes_gestionnaires(gestionnaires, comptes);
            Gere_operations_comptes (gestionnaires, comptes);


            
            foreach(var element in gestionnaires){
                Console.WriteLine($"{element.Key}:");
                foreach(var compte in element.Value.comptes){
                    Console.WriteLine(compte.Key);
                }
            }
            

        }
    }
}    
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
        
