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

// Programme qui génère les fichiers test d'entrée

namespace Projet_1{
    class Genere{
        public static void Genere_comptes(){
            // Pour compte
            Random r_cpt_aux = new Random();
            Random r_solde_aux = new Random();
            decimal r_solde;
            int r_cpt;
            using (StreamWriter w = File.CreateText("Comptes.csv")){
                for (int i = 0; i < 20; i++){
                    r_cpt = r_cpt_aux.Next(100);
                    r_solde = ((decimal)r_solde_aux.NextDouble()*2-1)*100000;
                    w.WriteLine($"{r_cpt};{r_solde}");
                }
            }
        }
        public static void Genere_transactions(){
            // Pour transactions
            Random r_type_aux = new Random();                   // Type de la transaction
            Random r_num_ind = new Random();                    // Indice pour les numeros de compte
            Random r_montant_aux = new Random();                // Random pour le montant de la transaction
            Random r_num_vir = new Random();                    // Random pour le num du virement

            int num_ind_1;                                        // Auxiliaire pour l'indice
            int num_ind_2;                                        // Auxiliaire pour l'indice
            int num_aux;                                        // Auxiliaire pour le numero de transaction
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
                    Random selector_1 = new Random();                           // Nombre random qui selectionne compte pour mettre à 0
                    Random selector_2 = new Random();                           // Meme chose pour compte destinataire

                    num_ind_1 = r_num_ind.Next(0,numero_compte.Count());      // On génère un indice aléatoire
                    num_ind_2 = r_num_ind.Next(0,numero_compte.Count());      // On génère un indice aléatoire
                    num_aux = r_num_vir.Next(0,1000);                        // Genere numéro de viremen aléatoire
                    montant_aux = (decimal)r_montant_aux.NextDouble()*1000000;// Génère montant aléatoire

                    if (selector_1.NextDouble()*15 < 1){
                        numero_compte[num_ind_1] = 0;
                    }
                    if(selector_2.NextDouble()*15 < 1){
                        numero_compte[num_ind_2] = 0;
                    }
                    w.WriteLine($"{num_aux};{montant_aux};{numero_compte[num_ind_1]};{numero_compte[num_ind_2]}");

                }
            }
        }
    }
}