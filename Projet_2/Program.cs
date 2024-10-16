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

            Charge comptes= new Charge();
            comptes.Tableau_comptes();

            Charge transactions = new Charge();
            transactions.Tableau_transactions();

            Charge gestionnaires = new Charge();
            gestionnaires.Tableau_gestionnaires();


            //Banque banque = new Banque(); 
            //banque.Traite_transactions();
            
        }
    }
}