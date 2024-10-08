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
    class Factor
    {   public static int Factorial(int N){
            // Manière iterative
            int N_fact = N;
            if (N_fact == 0){
                return 1;
            }
            else{
                for (int i = N-1; i > 0; i--){
                    N_fact *= i;
                
                }
            return N_fact; 
            }            
        }
        // La recursive c'est la méthode plus efficace
        public static int Recursive_factorial(int N){
            if (N == 0 || N == 1){
                return 1;
            }
            else{
                return N * Recursive_factorial(N - 1);
            }
        }                      
    }
}