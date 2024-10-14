using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_1
{
    public static class Program{
        public static void Main()
        {   string entree;
            Console.WriteLine("Pour generer des nouveaux fichiex test insérez GC pour un nouveau fichier de Compte et" 
            +"GT pour un nouveau fichier de transactions. Pour générer des transactions il faut que le fichiers comptes soit"+
            "créé");
            entree = Console.ReadLine();
            if(entree == "GC"){
                Genere.Genere_comptes();
            }
            else if(entree == "GT"){
                Genere.Genere_transactions();
            }
        }
    }
}