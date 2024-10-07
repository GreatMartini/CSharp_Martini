using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martini_CSharp.Serie1
{
    class Pow
    {
        public static void Puissance(int a, int b)
        {
            int resultat = 1;
            for(int i=0; i<b; i++)
            {
                resultat *= a;
            }
            Console.WriteLine($"{a}^{b}={resultat}");
        }
    }
}
