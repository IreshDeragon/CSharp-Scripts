using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RechercheNumero
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                /**
                 * 
                 * Définition des variables globales
                 * 
                 * */
                // Variable contenant le chemin d'accès au répertoire de départ de l'algorithme.
                string docPath = ConsoleApp2.Properties.Settings.Default.Chemin;
                Console.WriteLine(docPath);


                Console.WriteLine("Veuillez renseigner le numéro recherché :");
                string keyword = Console.ReadLine();
                Console.WriteLine("Potentiels chemins où se situe le fichier :");

                /**
                 * 
                 * Début du parcours de recherche d'un numéro.
                 * 
                 * */

                //récupération de tous les dossiers du répertoire défini dans la variable "docPath"
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(@docPath));

                //Première boucle permettant de parcourir tous les répertoires du dossier choisi dans la variable "docPath"
                //(ces répertoires sont les répertoires de campagne)
                foreach (string dir in dirs)
                {

                    string docPathD = dir;
                    //Console.WriteLine(docPathD);

                    //Pour chaque répertoire de campagne, on récupère dans un premier temps, les répertoires correspondant aux années
                    List<string> dirsYear = new List<string>(Directory.EnumerateDirectories(@docPathD));

                    foreach (string dirYear in dirsYear)
                    {
                        //Console.WriteLine(Path.GetDirectoryName(@dirYear + "\\"));

                        string[] splitparse = dirYear.Split('\\');
                        int vardirY = int.Parse(splitparse[splitparse.Length - 1]); //récupération du nom du répertoire sous la forme d'un "int"

                        string delPath = docPathD + "\\" + vardirY;
                        // récupération des répertoires correspondants aux mois
                        List<string> dirsMonth = new List<string>(Directory.EnumerateDirectories(delPath));

                        //boucle sur les répertoires correspondants aux mois. 
                        foreach (string dirMonth in dirsMonth)
                        {
                            //Console.WriteLine(dirMonth);
                            List<string> dirsDays = new List<string>(Directory.EnumerateDirectories(dirMonth));
                            foreach(string dirDay in dirsDays)
                            {
                                List<string> dirsOp = new List<string>(Directory.EnumerateDirectories(dirDay));
                                //Console.WriteLine(dirDay);
                                foreach (string op in dirsOp)
                                {
                                   string[] fileEntries = Directory.GetFiles(op);
                                    //Console.WriteLine(op);

                                    foreach (string fil in fileEntries)
                                    {
                                        if (@fil.Contains(keyword))
                                        {
                                            System.Diagnostics.Debug.WriteLine(@fil);
                                            Console.WriteLine(@fil);
                                        }
                                    }
                                }

                            }

                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"{dirsYear.Count} directories found.");
                }
                Console.WriteLine("fin du programme");
                string end = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

