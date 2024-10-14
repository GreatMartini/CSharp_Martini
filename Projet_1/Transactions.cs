using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Projet_1{

        // 4 clses différentes
    public class Transaction{
        private uint _num;
        uint expediteur;
        uint destinataire;
        decimal montant;

        public Transaction(char type, uint numero, uint exped, uint destin, decimal mont){
            _num = numero;
            expediteur = exped;
            destinataire = destin;
            montant = mont;

        }


    }
/*
    public class Retrait(){
        
    }
    public class Virement(){

    }
    public class Prelevement(){

    }

 */   
}