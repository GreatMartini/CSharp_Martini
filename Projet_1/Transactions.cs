using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace Projet_1{

        // 4 clses différentes
    public class Transaction{
        private char _type; 
        private uint _num;
        uint expediteur;
        uint destinataire;
        decimal montant;
        public string Depot(){
            //Compte exp = new Compte(expediteur);
            if (montant > 0){
                return "OK";
            }
            else{
                return "KO";
            }
            // Montant strictement positif

        }
        public string Retrait(){
            // M > 0, Solde > M
            // M < Max
            Compte exp = new Compte(expediteur);
            if(montant > 0 && (montant + exp.cumul_operations) < exp.retrait_max + montant && montant < exp.solde){
                return "OK";                
            }
            else{
                return "KO";
            }


        }
        public string Virement(){
            Compte exp = new Compte(expediteur);
            if(montant > 0 && (montant + exp.cumul_operations) < exp.retrait_max + montant && montant < exp.solde){
                return "OK";                
            }
            else{
                return "KO";
            }


        }
        public string Prelevement(){
            Compte des = new Compte(destinataire);
            if(montant > 0 && (montant + des.cumul_operations)< des.retrait_max + montant && montant < des.solde){
                return "OK";                
            }
            else{
                return "KO";
            }


        }

        public Transaction(char type, uint numero, uint exped, uint destin, decimal mont){
            _num = numero;
            expediteur = exped;
            destinataire = destin;
            montant = mont;
            // Règles par transaction:
            // D: depot, R: Retrait, V: virement, P: prelevement
            if (_type == 'D'){
                Depot();
            }
            else if(_type == 'R'){
                Retrait();
            } 
            else if(_type == 'V'){
                Virement();
            }
            else if(_type == 'P'){
                Prelevement();
            }  
            else{

                //throw new ArgumentException("Type de mouvement erroné");
            }
        }
    }
}