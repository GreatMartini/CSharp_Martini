using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data;
using System.Globalization;

namespace Martini_CSharp.Serie4{
    class Exercice_41{

        public static Dictionary < string, string > morse = new Dictionary<string, string>();
        public static void BuildMorse(){
            morse.Add("A", "=.===");
            morse.Add("B", "===.=.=.=");
            morse.Add("C", "===.=.===.=");
            morse.Add("D", "===.=.=");
            morse.Add("E", "=");
            morse.Add("F", "=.=.===.=");
            morse.Add("G", "===.===.=");
            morse.Add("H", "=.=.=.=");
            morse.Add("I", "=.=");
            morse.Add("J", "=.===.===.===");
            morse.Add("K", "===.=.===");
            morse.Add("L", "=.===.=.=");
            morse.Add("M", "===.===");
            morse.Add("N", "===.=");
            morse.Add("O", "===.===.===");
            morse.Add("P", "=.===.===.=");
            morse.Add("Q", "===.===.=.===");
            morse.Add("R", "=.===.=");
            morse.Add("S", "=.=.=");
            morse.Add("T", "===");
            morse.Add("U", "=.=.===");
            morse.Add("V", "=.=.=.===");
            morse.Add("W", "=.===.===");
            morse.Add("X", "===.=.=.===");
            morse.Add("Y", "===.=.===.===");
            morse.Add("Z", "===.===.=.=");
            // On utilise un dictionaire car on à besoin d'une structure
            // Non modifiale qui puisse être accessible par clefs
        }

        public static int LettersCount(string code){
            if(code.Length == 0){
                return 0;
            }
            else{
                int nletters = 1; 
                List <char> symbols = new List <char>();
                for(int i = 0; i < code.Length-2; i++){
                    symbols[0] = code[i];
                    symbols[1] = code[i+1]; 
                    symbols[2] = code[i+2];
                    if (symbols[0] == '.' && symbols[1] == '.' && symbols[2] == '.'){
                        nletters += 1;
                    }
                }
            return nletters;
            }
        }

        public static int WordsCount(string code){
            // Pour calculer le nombre de mots il suffit de compter les espaces
            if(code.Length == 0){
                return 0;
            }
            else{
                int nwords = 1; 
                List <char> symbols = new List <char>();
                for(int i = 0; i < code.Length-4; i++){
                    symbols[0] = code[i];
                    symbols[1] = code[i+1]; 
                    symbols[2] = code[i+2];
                    symbols[3] = code[i+3];
                    symbols[4] = code[i+4];
                    if (symbols[0] == '.' && symbols[1] == '.' && symbols[2] == '.' && symbols[3] == '.' && symbols[4] == '.'){
                        nwords += 1;
                    }
                }
            return nwords;
            }
        }

        public static string MorseTranslation(string code){
            // On doit traiter les cas de lettres et de mots 
            List <string> mots_morse = code.Split(".....").ToList();
            List <string> lettres_morse;
            string message_decode = "";

            BuildMorse();

            for (int i = 0; i < mots_morse.Count(); i++){
                lettres_morse = mots_morse[i].Split("...").ToList();
                for (int j = 0; j < lettres_morse.Count(); j++){
                    lettres_morse[j] = morse.FirstOrDefault(x => x.Value == lettres_morse[j]).Key;
                    message_decode += lettres_morse[j];
                }
            message_decode += " ";
            }
            return message_decode;
        }
    }
}