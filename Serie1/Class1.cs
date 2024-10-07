// Operations de base
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martini_CSharp.Serie1
{
    class Operations
    {      public static void Addition(int a, int b)
           {
            int somme = a + b;
            Console.WriteLine(a + "+" + b + "=" + somme);
            }

            public static void Soustraction(int a, int b)
            {

            int resultat = a - b;
            Console.WriteLine(a + "-" + b + "="+ resultat);
            }

            public static void Produit(int a, int b)
            {
            int resultat = a * b;
            Console.WriteLine(a + "*" + b + "=" + resultat);
            }

            public static void Quotien(int a, int b)
            {
            if (b != 0)
            {
                double resultat = a / b;
                Console.WriteLine(a + "/" + b + "=" + resultat);
            }
            else
            {
                Console.WriteLine(a + "/" + b + "= Opération invalide");
            }

        }

            

    }
}
