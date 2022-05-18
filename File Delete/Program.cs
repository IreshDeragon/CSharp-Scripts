using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraitementFichiers6mois
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
                string docPath = @"D:\Documents\records";
                System.Diagnostics.Debug.WriteLine(docPath);

                int year = DateTime.Today.Year; //Variable correspondant à l'année en cours
                int month = DateTime.Today.Month; //Variable correspondant au mois en cours


                /**
                 * 
                 * Début du parcours de suppression des fichiers et répertoires
                 * 
                 * */

                //récupération de tous les dossiers du répertoire défini dans la variable "docPath"
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(@docPath));

                //Première boucle permettant de parcourir tous les répertoires du dossier choisi dans la variable "docPath"
                //(ces répertoires sont les répertoires de campagne)
                foreach (string dir in dirs)
                {

                    string docPathD = dir;
                    System.Diagnostics.Debug.WriteLine(docPathD);

                    //Pour chaque répertoire de campagne, on récupère dans un premier temps, les répertoires correspondant aux années
                    List<string> dirsYear = new List<string>(Directory.EnumerateDirectories(@docPathD));

                    foreach (string dirYear in dirsYear)
                    {
                        System.Diagnostics.Debug.WriteLine(Path.GetDirectoryName(@dirYear + "\\"));

                        string[] splitparse = dirYear.Split('\\');
                        int vardirY = int.Parse(splitparse[splitparse.Length - 1]); //récupération du nom du répertoire sous la forme d'un "int"

                        if (vardirY == year) //si le répertoire = année en cours
                        {
                            string delPath = docPathD + "\\" + vardirY;
                            // récupération des répertoires correspondants aux mois
                            List<string> dirsMonth = new List<string>(Directory.EnumerateDirectories(delPath));

                            //boucle sur les répertoires correspondants aux mois. Si l'écart avec le mois en cours est >5, on supprime le répertoire.
                            foreach (string dirMonth in dirsMonth)
                            {
                                string[] splitparseM = dirMonth.Split('\\');
                                int vardirM = int.Parse(splitparseM[splitparseM.Length - 1]);
                                System.Diagnostics.Debug.WriteLine(dirMonth);
                                System.Diagnostics.Debug.WriteLine(month);
                                System.Diagnostics.Debug.WriteLine(vardirM);

                                if (month - vardirM > 5)
                                {

                                    Directory.Delete(dirMonth, true);
                                    System.Diagnostics.Debug.WriteLine("delete");
                                }
                            }
                        }
                        else if (vardirY == year - 1) // si le répertoire correspond à l'année précédente
                        {
                            string delPath = docPathD + "\\" + vardirY;
                            // récupération des répertoires correspondants aux mois
                            List<string> dirsMonth = new List<string>(Directory.EnumerateDirectories(delPath));

                            //boucle sur les répertoires correspondants aux mois. Si l'écart avec le mois en cours est >5, on supprime le répertoire.
                            foreach (string dirMonth in dirsMonth)
                            {
                                string[] splitparseM = dirMonth.Split('\\');
                                int vardirM = int.Parse(splitparseM[splitparseM.Length - 1]);
                                System.Diagnostics.Debug.WriteLine(dirMonth);
                                System.Diagnostics.Debug.WriteLine(month);
                                System.Diagnostics.Debug.WriteLine(vardirM);

                                if (12 - vardirM + month > 5)
                                {
                                    Directory.Delete(dirMonth, true);
                                    System.Diagnostics.Debug.WriteLine("delete");
                                }
                            }
                        }
                        else // si le répertoire correspond à un année antérieur à l'année précédente, on le supprime.
                        {
                            Directory.Delete(dirYear, true);
                            System.Diagnostics.Debug.WriteLine("delete");
                        }
                    }
                    System.Diagnostics.Debug.WriteLine($"{dirsYear.Count} directories found.");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
