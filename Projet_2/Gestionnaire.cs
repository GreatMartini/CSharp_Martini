using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_2
{
    public class Gestionnaire{

        private uint _num_gest;
        public string type;
        public uint transactions;

        public Dictionary <uint, Compte> comptes;


        public Gestionnaire(uint num, string t, uint trans){
            _num_gest = num;
            type = t;
            transactions = trans;
        }
    }
}