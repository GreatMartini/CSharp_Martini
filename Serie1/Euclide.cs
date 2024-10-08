// Operations de base
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Martini_CSharp.Serie1
{
    class Euclide
    {   public static int Algorithme(int a, int b){
            // Manière iterative
            int q = a / b;
            int r = a % b;
            if (r == 0){
                return q;
            }   
        return Algorithme(b, r);         
        }                                        
    }
}