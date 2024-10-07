using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martini_CSharp.Serie1
{
    class Division_entiere
    {
        public static void IntegerDivision(int a, int b)
        {
            int q = a / b;
            int r = a % b;
            if (r == 0)
            {
                Console.WriteLine($"{a} = {q} * {b}");
            }
            else
            {
                Console.WriteLine($"{a} = {q} * {b} + {r}");
            }
            
        }
    }
}
