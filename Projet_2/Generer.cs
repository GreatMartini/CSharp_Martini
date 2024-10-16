using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Formats.Asn1;
using Microsoft.Data.Analysis;
// Programme qui génère les fichiers test d'entrée

namespace Projet_2{
    class Genere{

        public static void Genere_comptes(){
            // Declare des randoms pour creer fichier
            Random r_ID = new Random();
            Random r_date = new Random();
            Random r_solde = new Random();
            Random r_entree = new Random();
            Random r_sortie = new Random();

            // Declare des variable pour ecrire
            int ID, date_j, date_m, date_a , entree, sortie;
            //DateTime date = new DateTime();
            decimal solde;

            using (StreamWriter w = File.CreateText("Comptes.csv")){
                for (int i = 0; i < 30; i++){
                    ID = r_ID.Next(100);                                // Crée un nouveau num compte
                    date_m = r_date.Next(1,13);                         // Crée un nouveau mois aléatoirement
                    date_a = r_date.Next(1582, 2024);                   // Crée une nouvelle année dans le calendrier grégorien
                    if(date_m == 1 || date_m == 3 || date_m == 5 || date_m == 7 || date_m == 8 || date_m == 10 || date_m == 12){
                        date_j = r_date.Next(1,32);
                    }
                    else if(date_m == 2){
                        if(date_a % 4 == 0){
                            date_j = r_date.Next(1,30);
                        }
                        else{
                            date_j = r_date.Next(1,29);
                        }
                    }
                    else{
                        date_j = r_date.Next(1,31);
                    }   
                    
                    DateTime date = new DateTime(date_a, date_m, date_j);

                    solde = ((decimal)r_solde.NextDouble()*2-1)*1000;   // Crée un solde négatif ou positif
                    entree = r_entree.Next(0,5);
                    sortie = r_sortie.Next(0,5);

                    if (entree == 0 && sortie == 0){
                        w.WriteLine($"{ID};{date.ToString("dd/MM/yyyy")};{solde};;"); 
                    }
                    else if(entree == 0 && sortie != 0){
                        w.WriteLine($"{ID};{date.ToString("dd/MM/yyyy")};{solde};;{sortie}"); 
                    }
                    else if(entree != 0 && sortie == 0){
                        w.WriteLine($"{ID};{date.ToString("dd/MM/yyyy")};{solde};{entree};"); 
                    }
                    else{
                        w.WriteLine($"{ID};{date.ToString("dd/MM/yyyy")};{solde};{entree};{sortie}"); 
                    }
                }
            }

        }

        // On genere le fichier gestionnaires
        public static void Genere_gestionnaires(){
            Random r_type = new Random();
            Random r_transactions = new Random();

            int ID, transactions;
            double type;
            string stype;
            using (StreamWriter w = File.CreateText("Gestionnaires.csv")){

                for (int i = 1; i < 5; i++){
                    ID = i;                                         // Crée un nouveau num compte
                    type = r_type.NextDouble();
                                                                    // Crée le type aleatoirement
                    if (type < 0.5){
                        stype = "Particulier";
                    }
                    else{
                        stype = "Entreprise";
                    }

                    transactions = r_transactions.Next(11);         // Crée le num de transactions aleatoirement
                    w.WriteLine($"{ID};{stype};{transactions}");    // On écrit
                }
            }
        }
        public static void Genere_transactions(){
            // Pour transactions
            Random r_type_aux = new Random();                   // Type de la transaction
            Random r_num_ind = new Random();                    // Indice pour les numeros de compte
            Random r_montant_aux = new Random();                // Random pour le montant de la transaction
            Random r_num_vir = new Random();                    // Random pour le num du virement
            Random r_date = new Random();


            int num_ind_1;                                        // Auxiliaire pour l'indice
            int num_ind_2;                                        // Auxiliaire pour l'indice
            int num_aux;                                        // Auxiliaire pour le numero de transaction
            int date_j, date_m, date_a;                         // Annee mois et jour 
            decimal montant_aux;                                // Auxiliaire pour le montant

            List <int> numero_compte = new List<int>();
                if (File.Exists("Comptes.csv")){
                    using (StreamReader r = new StreamReader("Comptes.csv")){
                        while(!r.EndOfStream){
                            var line = r.ReadLine();
                            var values = line.Split(';');
                            num_aux = Convert.ToInt32(values[0]);
                            numero_compte.Add(num_aux);
                        }                    
                    }
                }
                else{
                    throw new Exception("Comptes.csv non existant");
                }
                
            using (StreamWriter w = File.CreateText("Transactions.csv")){
                for (int i = 0; i < 100; i++){
                    date_m = r_date.Next(1,13);                         // Crée un nouveau mois aléatoirement
                    date_a = r_date.Next(1582, 2024);                   // Crée une nouvelle année dans le calendrier grégorien
                    if(date_m == 1 || date_m == 3 || date_m == 5 || date_m == 7 || date_m == 8 || date_m == 10 || date_m == 12){
                        date_j = r_date.Next(1,32);
                    }
                    else if(date_m == 2){
                        if(date_a % 4 == 0){
                            date_j = r_date.Next(1,30);
                        }
                        else{
                            date_j = r_date.Next(1,29);
                        }
                    }
                    else{
                        date_j = r_date.Next(1,31);
                    }   
                    
                    DateTime date = new DateTime(date_a, date_m, date_j);
                    
                    Random selector_1 = new Random();                           // Nombre random qui selectionne compte pour mettre à 0
                    Random selector_2 = new Random();                           // Meme chose pour compte destinataire

                    num_ind_1 = r_num_ind.Next(0,numero_compte.Count());      // On génère un indice aléatoire
                    num_ind_2 = r_num_ind.Next(0,numero_compte.Count());      // On génère un indice aléatoire
                    num_aux = r_num_vir.Next(0,1000);                        // Genere numéro de viremen aléatoire
                    montant_aux = (decimal)r_montant_aux.NextDouble()*100;// Génère montant aléatoire

                    if (selector_1.NextDouble()*15 < 1){
                        numero_compte[num_ind_1] = 0;
                    }
                    if(selector_2.NextDouble()*15 < 1){
                        numero_compte[num_ind_2] = 0;
                    }
                    w.WriteLine($"{num_aux};{date.ToString("dd/MM/yyyy")};{montant_aux};{numero_compte[num_ind_1]};{numero_compte[num_ind_2]}");

                }
            }
        }
    }
}