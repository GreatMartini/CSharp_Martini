﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Martini_CSharp.Serie1;
using Martini_CSharp.Serie2;
using Martini_CSharp.Serie4;

namespace Martini_CSharp
{
    public static class Program
    { 
        public static void Serie1(){
            int A, B;
            string entree;
            string oper;
            
            Console.WriteLine("Introduire le premier nombre");
            entree = Console.ReadLine();
            bool entree_A = int.TryParse(entree, out A);

            Console.WriteLine("Introduire le deuxième nombre");
            entree = Console.ReadLine();
            bool entree_B = int.TryParse(entree, out B);

            Console.WriteLine("Indiquer l'operateur");
            oper = Console.ReadLine();
            if (oper == "+")
            {
                Operations.Addition(A, B);
            }
            else if (oper == "-")
            {
                Operations.Soustraction(A, B);
            }
            else if (oper == "*")
            {
                Operations.Produit(A, B);
            }
            else if (oper == "/")
            {
                Operations.Quotien(A, B);
            }
            else
            {
                Console.WriteLine($"{A} {oper} {A} = Opération invalide");
            }
            Console.ReadKey();

            // Division entiere:
            Console.WriteLine("Division entière");

            Console.WriteLine("Introduire le premier nombre");
            entree = Console.ReadLine();
            entree_A = int.TryParse(entree, out A);

            Console.WriteLine("Introduire le deuxième nombre");
            entree = Console.ReadLine();
            entree_B = int.TryParse(entree, out B);
            if (B != 0)
            {
                Division_entiere.IntegerDivision(A, B);
            }
            else
            {
                Console.WriteLine(A + "/" + B + "= Opération invalide");
            }
            Console.ReadKey();

            // Fonction pow:

            Console.WriteLine("Fonction puissance");

            Console.WriteLine("Introduire le premier nombre");
            entree = Console.ReadLine();
            entree_A = int.TryParse(entree, out A);

            Console.WriteLine("Introduire le deuxième nombre");
            entree = Console.ReadLine();
            entree_B = int.TryParse(entree, out B);
            if (B > 0){
                Pow.Puissance(A, B);
            }
            int heure;
            Console.ReadKey();
            Console.WriteLine("Horloge parlante: Introduire un entier de 0 à 23");
            entree = Console.ReadLine();
            bool entree_h = int.TryParse(entree, out heure);
            Console.WriteLine(Horloge.Parle(heure));

            Console.ReadKey(); 

            int N;
            //Console.WriteLine("Pyramide: Veuillez introduire un nombre d'etages");
            entree = Console.ReadLine();
            bool entree_N = int.TryParse(entree, out N);
            Pyramide.Construire(N, true); // true pour stillée false pour lise
            Console.ReadKey();

            // Factorielle:
            Console.WriteLine("Factorielle: Veuillez introduire un nombre pour calculer sa factorielle");
            entree = Console.ReadLine();
            entree_N = int.TryParse(entree, out N);
            Console.WriteLine(Factor.Factorial(N));
            Console.ReadKey();

            // Factorielle recursive:
            Console.WriteLine("Factorielle recursive: Veuillez introduire un nombre pour calculer sa factorielle");
            entree = Console.ReadLine();
            entree_N = int.TryParse(entree, out N);
            Console.WriteLine(Factor.Recursive_factorial(N));
            Console.ReadKey();             

            // Determination de nombre premier
            Console.WriteLine("Nombre premier: Veuillez introduire un nombre");
            entree = Console.ReadLine();
            entree_N = int.TryParse(entree, out N);
            Console.WriteLine(N_premiers.Determination(N));
            Console.ReadKey(); 

            // Affichage nombre premiers
            Console.WriteLine("Nombre premier: Liste de 1 à 100");

            N_premiers.Affiche();
            Console.ReadKey(); 

            Console.WriteLine("Algorithme D'Euclide: Insérez un nombre a:");
            entree = Console.ReadLine();
            entree_A = int.TryParse(entree, out A);
            Console.WriteLine("Insérez un nombre b:");
            entree = Console.ReadLine();
            entree_B = int.TryParse(entree, out B);
            Console.WriteLine(Euclide.Algorithme(A, B));
            Console.ReadKey();         
        }

        public static void Serie2(){
            Console.WriteLine("Exercice 1 recherche linéaire");
            int[] tab1 = {1, -5 ,10, -3, 0, 4, 2, -7};
            Console.WriteLine($"Indice de 2: {Exercice_21.LinearSearch(tab1, 2)}");
            Console.WriteLine($"Indice de -8: {Exercice_21.LinearSearch(tab1, -8)}");
            // Dans le pire des cas les éléments lus correspondent à la taille du tableau

            Console.WriteLine("Exercice 1 recherche dichotomique");
            Console.WriteLine($"Indice de 2: {Exercice_21.BinarySearch(tab1, 2)}");
            Console.WriteLine($"Indice de -8: {Exercice_21.BinarySearch(tab1, -8)}");
            // Dans le pire des cas les éléments lus seront N/2

            // Exercice 2
            int[] u = {1, 2, 3};
            int[] v = {-1, -4, 0};
            int[][] matrice = Exercice_22.BuildingMatrix(u, v); 
            for(int i = 0; i < matrice.Length; i++){
                foreach(int item in matrice[i]){
                    Console.Write(item + " ");
                }
                Console.Write("\n");
            }

            int[][] matrice1 = new int [3][];
            int[][] matrice2 = new int [3][];

            matrice1[0] = [1, 2];
            matrice1[1] = [4, 6];
            matrice1[2] = [-1, 8];

            matrice2[0] = [-1, 5];
            matrice2[1] = [-4, 0];
            matrice2[2] = [0, 2];


            // Addition de matrices
            int[][] matrice_resultante = Exercice_22.Addition(matrice1, matrice2);
            for(int i = 0; i < matrice_resultante.Length; i++){
                foreach(int item in matrice_resultante[i]){
                    Console.Write(item + " ");
                }
                Console.Write("\n");
            }

            // Soustraction de matrices
            matrice_resultante = Exercice_22.Substraction(matrice1, matrice2);
            for(int i = 0; i < matrice_resultante.Length; i++){
                foreach(int item in matrice_resultante[i]){
                    Console.Write(item + " ");
                }
                Console.Write("\n");
            }

            // Multiplication de matrices:

            
            int[][] matrice3 = new int [2][];
            matrice3[0] = [-1, 5, 0];
            matrice3[1] = [-4, 0, 1];
            int [][] matrice_produit = Exercice_22.Multiplication(matrice1, matrice3);
            for(int i = 0; i < matrice_produit.Length; i++){
                foreach(int item in matrice_produit[i]){
                    Console.Write(item + " ");
                }
                Console.Write("\n");
            }

            int [] primes = Exercice_23.ErastorhenesSieve(100);
            foreach (int item in primes){
                Console.Write(item + " ");
            }
            
            int points = 0;
            int total_points = 6;
            // 1ere question
            string question = "Quelle est l'anée de naissance de Beethoven?";
            List <string> reponses = ["1458", "1763", "1770", "1810"];
            int solution = 3;
            int weight = 3;
            var qcm1 = new exercice_24.Qcm(question, reponses, solution, weight);
            
            points += exercice_24.AskQuestion(qcm1);

            question = " Quelle est l'année de naissance de Néron?";
            reponses = ["73 Av.C.", "21 Av.C.", "24 Ap.C.", "37 Ap.C"];
            solution = 4;
            weight = 2;

            var qcm2 = new exercice_24.Qcm(question, reponses, solution, weight);
            points += exercice_24.AskQuestion(qcm2);

            question = " Quelle est l'année de naissance d'Atila'?";
            reponses = [" 0", "234.", "395", "402"];
            solution = 3;
            weight = 1;

            var qcm3 = new exercice_24.Qcm(question, reponses, solution, weight);
            points += exercice_24.AskQuestion(qcm3);

            Console.Write($"Résultat du questionnaire: {points}/{total_points}");
        }

        public static void Serie4(){
            // Code Morse
            string message_morse =
            "===.=.===.=...=...===.=...=.=...===.===.=.=...=.===" +
            ".....=...=.=.=.....=.===.=.=...===.===.===.....===.===...=...=.===.===.===...===.===.===...=.===.=";
            Exercice_41.BuildMorse();
            try{
                string message_decode = Exercice_41.MorseTranslation(message_morse);
                Console.WriteLine(message_decode);
            }
            catch(ArgumentException ex){
                Console.WriteLine(ex.ToString());
            };

            try{
                string message_decode = Exercice_41.EfficientMorseTranslation(message_morse);
                Console.WriteLine(message_decode);
            }
            catch(ArgumentException ex){
                Console.WriteLine(ex.ToString());
            };

            //string message_crypte = Exercice_41.MorseEncryption("Ceniza es lo mejor");
            //Console.WriteLine(message_crypte);

              
        }
        public static void Main()
        {
            //Serie1();
            //Serie2();
            Serie4();

        }
    }
}
