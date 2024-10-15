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
        Compte expediteur;
        Compte destinataire;
        public decimal montant;
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
            if(montant > 0 && (montant + expediteur.cumul_operations) < expediteur.retrait_max + montant && montant < expediteur.solde){
                return "OK";                
            }
            else{
                return "KO";
            }


        }
        public string Virement(){
            if(montant > 0 && (montant + expediteur.cumul_operations) < expediteur.retrait_max + montant && montant < expediteur.solde){
                return "OK";                
            }
            else{
                return "KO";
            }


        }
        public string Prelevement(){
            if(montant > 0 && (montant + destinataire.cumul_operations)< destinataire.retrait_max + montant && montant < destinataire.solde){
                return "OK";                
            }
            else{
                return "KO";
            }


        }

        public Transaction(char type, uint numero, Compte exped, Compte destin, decimal mont){
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