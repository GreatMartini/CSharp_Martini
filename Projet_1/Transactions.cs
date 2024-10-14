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
        private void Depot(){
            //Compte exp = new Compte(expediteur);
            if (montant < 0){
                throw new ArgumentException("Montant est inférieur à 0.");
            }
            // Montant strictement positif

        }
        private void Retrait(){
            // M > 0, Solde > M
            // M < Max
            Compte exp = new Compte(expediteur);
            if(montant < 0){
                throw new ArgumentException("Montant est inférieur à 0.");                
            }
            else if(montant > exp.retrait_max + montant){
                throw new ArgumentException("Montant dépasse la capacité actuelle");
            }
            else if(montant > exp.solde){
                throw new ArgumentException("Le montant dépasse les ressources du compte");
            }


        }
        private void Virement(){
            Compte exp = new Compte(expediteur);
            if(montant < 0){
                throw new ArgumentException("Montant est inférieur à 0.");                
            }
            else if(montant > exp.retrait_max + montant){
                throw new ArgumentException("Montant dépasse la capacité actuelle");
            }
            else if(montant > exp.solde){
                throw new ArgumentException("Le montant dépasse les ressources du compte");
            }


        }
        private void Prelevement(){
            Compte dest = new Compte(destinataire);
            if(montant < 0){
                throw new ArgumentException("Montant est inférieur à 0.");                
            }
            else if(montant > dest.retrait_max + montant){
                throw new ArgumentException("Montant dépasse la capacité actuelle");
            }
            else if(montant > dest.solde){
                throw new ArgumentException("Le montant dépasse les ressources du compte");
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
            else if(_type == 'D'){
                Retrait();
            } 
            else if(_type == 'V'){
                Virement();
            }
            else if(_type == 'P'){
                Prelevement();
            }  
            else{
                throw new ArgumentException("Type de mouvement erroné");
            }
        }
    }
}