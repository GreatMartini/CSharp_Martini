using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_1
{
    public class Compte{
        // La classe compte doit comporter un numéro de compte
        // Un solde
        //un maximum de retrait autorisé
        // Un historique des transactions
        private uint _num_ctp;
        public decimal solde;
        public decimal retrait_max;
        private decimal _cumul_operations;

        public Compte(uint num, decimal sol = 0, decimal rmax = 1000, params decimal[] h_transactions){
            _num_ctp = num;
            solde = sol;
            retrait_max = rmax;

        }
    }
}