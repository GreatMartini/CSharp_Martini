// Operations de base
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Martini_CSharp.Serie2{
    class Exercice_1{
        public static int LinearSearch(int[] tableau, int valeur){
            int taille = tableau.Length;
            if (taille == 0){
                return -1;
            }
            for(int i = 0;  i < taille; i++){
                if (tableau[i] == valeur)
                {
                    return i;
                }
            }
        return -1;
        }

        public static int BinarySearch(int[] tableau, int valeur){
            Array.Sort(tableau);
            int taille = tableau.Length;
            int ig = 0;                         // Indice de gauche
            int im;                              // Indice du milieu
            int id = taille - 1;                    // Indice de droite du tableau

            while(id >= ig){
                    im = (id + ig)/2;
                    if(valeur == tableau[ig]){
                        return ig;
                    }
                    else if(valeur == tableau[id]){
                        return id;
                    }
                    else if(valeur == tableau[im]){
                        return im;
                    }
                    else if (valeur < tableau[im]){
                        id = im - 1;
                    }
                    else if (valeur > tableau[im]){
                        ig = im + 1; 
                    }
                } 
            return -1;                  
        }
    }

    class Exercice_2{
        public static int [][] BuildingMatrix(int[] leftVector, int[] rightVector){
            int rang = rightVector.Length;
            int dimension = leftVector.Length;
            int[][] matrice = new int [dimension][];
            for(int i = 0; i < dimension; i++){
                matrice[i] = new int[rang];
                for(int j = 0; j < rang; j++){
                    matrice[i][j] = leftVector[i]*rightVector[j];
                }
            }
            return matrice;
        }
        
        public static int [][] Addition(int[][] leftMatrix, int[][] rightMatrix){
            int[][] matrice = new int [leftMatrix.Length][];
            for(int i = 0; i < leftMatrix.Length; i++){
                matrice[i] = new int [leftMatrix[i].Length]; 
                for(int j = 0; j < leftMatrix[i].Length; j++){
                    matrice[i][j] = leftMatrix[i][j] + rightMatrix[i][j];
                }
            }
            return matrice;

        }

        public static int [][] Substraction(int[][] leftMatrix, int[][] rightMatrix){
            int[][] matrice = new int [leftMatrix.Length][];
            for(int i = 0; i < leftMatrix.Length; i++){
                matrice[i] = new int [leftMatrix[i].Length]; 
                for(int j = 0; j < leftMatrix[i].Length; j++){
                    matrice[i][j] = leftMatrix[i][j] - rightMatrix[i][j];
                }
            }
            return matrice;

        }

        public static int [][] Multiplication(int[][] leftMatrix, int[][] rightMatrix){
            int[][] matrice = new int [leftMatrix.Length][];
            for(int i = 0; i < leftMatrix.Length; i++){
                matrice[i] = new int [leftMatrix.Length]; 
                for(int j = 0; j < rightMatrix[i].Length; j++){
                    int sum = 0;
                    for (int k = 0; k < leftMatrix.Length; k++){
                        sum += leftMatrix[i][k] * rightMatrix[k][k];
                    }
                matrice[i][j] = sum;                    
                }
            }
            return matrice;

        }
    }

}