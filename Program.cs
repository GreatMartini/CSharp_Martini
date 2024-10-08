using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Martini_CSharp.Serie1;

namespace Martini_CSharp
{
    public static class Program
    { 
 
        public static void Main()
        {
            int A, B;
            string entree;
            string oper;
            /*
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
            */
            int N;
            Console.WriteLine("Pyramide: Veuillez introduire un nombre d'etages");
            entree = Console.ReadLine();
            bool entree_N = int.TryParse(entree, out N);
            Pyramide.Construire(N, true);
            
            
        }
    }
}
