using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace Projet_1{
    public class Banque{
        // On va faire deux structures, chacune correspondants à un des fichiers
        
        public void Traite_transactions(){
                if (File.Exists("Transactions.csv")){
                    List <int> codes_transaction = new List <int>();
                    using (StreamReader r = new StreamReader("Transaction.csv")){
                        while(!r.EndOfStream){
                            var line = r.ReadLine();
                            var values = line.Split(';');
                        }                    
                    }
                }
                else{
                    throw new Exception("Transactions.csv non existant");
                }
        }
    }
}