using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Net;
using System.Linq.Expressions;
using Deedle;

namespace Projet_2{
    public class Scribe{
        public void Ecrit_statut_comptes(uint code, DateTime date, decimal solde, uint entree, uint sortie, string status){                                       // Ecrit le fichier de sortie status
            using (StreamWriter w = new StreamWriter("Status_comptes.csv", true)){
                w.WriteLine($"{code};{date};{solde};{entree};{sortie};{status}");               
            }
        }
        public void Ecrit_statut_transactions(uint code, DateTime date, decimal montant, uint expediteur, uint destinataire, string status){                                       // Ecrit le fichier de sortie status
            using (StreamWriter w = new StreamWriter("Status_transactions.csv", true)){
                w.WriteLine($"{code};{date};{montant};{expediteur};{destinataire};{status}");               
            }
        }

        //public void Ecrit_metrologie(){
        //    
        //}
 
    }        
}