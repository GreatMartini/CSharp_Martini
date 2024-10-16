using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2
{
    public class Compte{
        // La classe compte doit comporter un numéro de compte
        // Un solde
        //un maximum de retrait autorisé
        // Un historique des transactions
        private uint _num_ctp;
        public decimal solde;
        public decimal retrait_max;
        public decimal cumul_operations;
        public DateTime date;
        public uint entree; // Peut etre vide
        public uint sortie;  // Peut etre vide

        public List <decimal> historique_transactions = new List<decimal>();

        public Compte(uint num, DateTime dat , uint ent, uint sor, decimal sol = 0, decimal rmax = 1000){
            _num_ctp = num;
            date = dat;
            solde = sol;
            entree = ent;
            sortie = sor;
            retrait_max = rmax; 
        }
    }
}