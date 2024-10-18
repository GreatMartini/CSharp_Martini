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
            
            Genere genere = new Genere();
            genere.Genere_transactions();
            genere.Genere_comptes();
            genere.Genere_gestionnaires();

            Banque banque = new Banque();
            banque.Traitement();

            //Banque banque = new Banque(); 
            //banque.Traite_transactions();
            
        }
    }
}