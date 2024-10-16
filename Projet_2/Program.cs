using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2
{
    public static class Program{
        public static void Main(){
            //Genere.Genere_comptes();
            //Genere.Genere_transactions();

            Banque banque = new Banque(); 
            banque.Traite_transactions();
            
        }
    }
}