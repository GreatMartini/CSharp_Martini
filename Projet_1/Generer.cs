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
            //if (!File.Exists("Comptes.csv")){
                using (StreamWriter w = File.CreateText("Comptes.csv")){
                    for (int i = 0; i < 20; i++){
                        r_cpt = r_cpt_aux.Next(100);
                        r_solde = (decimal)r_solde_aux.NextDouble()*2-1;
                        w.WriteLine($"{r_cpt};{r_solde}");
                    }
                }
            
            //}

        }
        public static void Genere_transactions(){
            // Pour transactions
            Random r_type_aux = new Random();
            Random r_num_ind = new Random();
            int num_aux;
            Random r_montant_aux = new Random();
            List <int> numero_compte = new List<int>();
                if (File.Exists("Comptes.csv")){
                    using (StreamReader r = new StreamReader("Comptes.csv")){
                        while(!r.EndOfStream){
                            var line = r.ReadLine();
                            var values = line.Split(';');
                            num_aux = Convert.ToInt32(values[0]);
                            numero_compte.Add(num_aux);
                        }
                    
                    //numero_compte = (int)numero_compte;
                    }
                }
                else{
                    throw new Exception("Comptes.csv non existant");
                }
                
            using (StreamWriter w = File.CreateText("Transactions.csv")){
                for (int i = 0; i < 100; i++){
                    //r_cpt = r_cpt_aux.Next(100);
                    //r_solde = (decimal)r_solde_aux.NextDouble()*2-1;
                }
            }


        }
    }
}