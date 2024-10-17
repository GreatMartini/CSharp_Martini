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
    public class Charge{

/*
        public Dictionary <uint, Compte> Tableau_comptes(){
            
            Dictionary <uint, Compte> comptes = new Dictionary<uint, Compte>();
            if (File.Exists("Comptes.csv")){
                using (StreamReader r = new StreamReader("Comptes.csv")){
                    while(!r.EndOfStream){

                        decimal solde = 0;
                        var line = r.ReadLine();
                        var values = line.Split(';');
                        uint entree = 0;
                        uint sortie = 0;
                        // On fait les parse
                        bool code_correct = uint.TryParse(values[0], out uint code);
                        bool date_correcte = DateTime.TryParseExact(values[1],"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
                        bool solde_correct = decimal.TryParse(values[2], NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out solde);
                        
                        // Bool pour l'entrée
                        bool entree_correcte = false;
                        if(values[3] == ""){
                            //entree_correcte = uint.TryParse(values[3], out entree);
                            entree_correcte = true;
                            //entree = 0;
                        }
                        else{
                            entree_correcte = uint.TryParse(values[3], out entree);
                        }

                        // Bool pour la sortie
                        bool sortie_correcte = false;
                        if(values[4] == ""){
                            //sortie_correcte = uint.TryParse(values[4], out uint sortie);
                            sortie_correcte = true;
                            //sortie = 0;
                        }
                        else{
                            sortie_correcte = uint.TryParse(values[4], out sortie);
                        }
                        
                        // Bool pour solde positif
                        bool solde_positif = false;

                        if(solde >= 0){
                            solde_positif = true;
                        }

                        // Si le solde est vide on continue
                        if(values[2] == ""){
                            solde_correct = true;
                        }                        

                        if (code_correct && date_correcte && solde_positif && solde_correct && entree_correcte && sortie_correcte && !comptes.ContainsKey(code)){
                            Compte compte_i = new Compte(code, date, entree, sortie, solde);
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
        }*/
        
        public List< List <string>> Tableau_comptes(){
            List <List<string>> comptes = new List<List<string>>();   
            if (File.Exists("Comptes.csv")){
                using (StreamReader r = new StreamReader("Comptes.csv")){
                    while(!r.EndOfStream){
                        List <string> ligne_compte = new List<string>();
                        var line = r.ReadLine();
                        var values = line.Split(';');
                        ligne_compte.Add(values[0]);
                        ligne_compte.Add(values[1]);
                        ligne_compte.Add(values[2]);
                        ligne_compte.Add(values[3]);
                        ligne_compte.Add(values[4]);
                        ligne_compte.Add("OK");
                        comptes.Add(ligne_compte);
                    }
                }
            return comptes;
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
                        ligne_transaction.Add(values[4]);
                        ligne_transaction.Add("OK");
                        transactions.Add(ligne_transaction);
                    }
                }
            return transactions;
            }
            else{
                throw new Exception("Transactions.csv non existant");
            }
        }
    
        public Dictionary <uint, Gestionnaire> Tableau_gestionnaires(){
            
            Dictionary <uint, Gestionnaire> gestionnaires = new Dictionary<uint, Gestionnaire>();
            if (File.Exists("Gestionnaires.csv")){
                using (StreamReader r = new StreamReader("Gestionnaires.csv")){
                    while(!r.EndOfStream){

                        var line = r.ReadLine();
                        var values = line.Split(';');
                        string type = values[1];
                        // On fait les parse
                        bool code_correct = uint.TryParse(values[0], out uint code);
                        bool type_correct = false;
                        bool transactions_correct = uint.TryParse(values[2], out uint transactions);

                        // On teste si le type est correct:

                        if (type == "Entreprise" || type == "Particulier"){
                            type_correct = true;
                        }                

                        if (code_correct && type_correct && transactions_correct && !gestionnaires.ContainsKey(code)){
                            Gestionnaire gestionnaire_i = new Gestionnaire(code, type, transactions);
                            gestionnaires.Add(code, gestionnaire_i);
                        }
                        else{
                            continue;
                        }  
                    }
                    return gestionnaires;
                }


            }
            else{
                throw new Exception("Gestionnaires.csv non existant");
                }
        }    
    }        
}