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
    class N_premiers

    {     
        public static bool Determination(int N){
                int[] bases = {2, 3, 5, 7};
                bool est_prime = false; 
                foreach (int item in bases){
                    if(N % item == 0 && N != item){
                        break;
                    }
                    else{
                        est_prime = true;
                    }
                }
            return est_prime;            
        } 

        public static void Affiche(){
                int[] bases = {2, 3, 5, 7};
                for (int i = 1; i <= 100; i++){
                    foreach (int item in bases)
                        if(i % item == 0 && i != item){
                            break;
                        }
                        else{
                            Console.WriteLine(i);
                            break;
                        }                    
                    }
                }    
        }                       
    }
