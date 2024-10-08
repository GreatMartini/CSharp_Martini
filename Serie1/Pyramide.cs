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
    class Pyramide
    {   public static void Construire(int N, bool s){
            // s est vrai si la pyramide est striée
            // Le nombre de blocs au niveau N est 2N-1
            // Le nombre total de blocs donné est SUM(2i-1)_i^N = N**2
            // La position du sommet de la pyramide est j = N-2 (car on part de 0)
            // gauche(j) = gauche (j-1)-1 et droite(j) = droite(j-1)+1
            // En fonction de N on a: gauche(j) = N - j - 1 et droite(j) = N + j - 1
            string[] etage;             //Declaration d'un etage
            etage = new string[2*N-1];
            int pos_g = N-1;              // Initialisation de la position de gauche
            int pos_d = N-1;              // Initialisation de la positiond de droite
            for(int i = 1; i < N+1; i++){
                for (int j = 0; j < 2*N-1; j++){
                    if (j >= pos_g && j <= pos_d){
                        if (s == true && i%2 == 0){
                        etage[j] = "-";
                        }
                        else if (s == true && i%2 != 0){
                        etage[j] = "+";
                        }
                        else{
                        etage[j] = "+";
                        }
                    }
                    else{
                        etage[j] = " ";
                    }
                    Console.Write(etage[j]);
                }
            Console.Write("\n");
            pos_g = N -1 - i;
            pos_d = N -1 + i;
            }
        }                      
    }
}