// Operations de base
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martini_CSharp.Serie1
{
    class Horloge
    {   public static string Parle(int heure){
            string h;
            if (heure >= 0 && heure < 6){
                h = "Merveilleuse nuit!";
            }
            else if (heure >= 6 && heure < 12){
                h = "Bonne matinée!";
            }
            else if (heure == 12){
                h = "Bon appétit!";
            }
            else if (heure >= 13 && heure <= 18){
                h = "Profitez de votre après midi!";
            }
            else if (heure > 18){
                h = "Passez un bonne soirée!";
            }
            else{
                h = "Un jour n'a que 24 heures, de 0 à 23";
            }

            return h;

        }
        


                      
    }
}
