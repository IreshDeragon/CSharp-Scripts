using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformationAuFormatCSV
{
    class Program
    {
        static void Main(string[] args)
        {
            string docPath = @"C:\Users\User\Documents\TESTCSV";

            string[] fileEntries = Directory.GetFiles(docPath);
            Console.WriteLine("Indiquez le nombre de colonnes :");
            string entree = Console.ReadLine();
            int nbcol = int.Parse(entree);

            foreach (string fil in fileEntries)
            {
                string text = File.ReadAllText(fil);
                string[] subs = text.Split(' ');

                int cpt = 0;
                List<string> res = new List<string>();

                string pop = "";
                foreach (string s in subs)
                {
                    if (s != "" && s!= "\n")
                    {
                        //string s2 = s.Replace("\n", string.Empty);
                        System.Diagnostics.Debug.WriteLine("ok1");
                        
                        if(cpt%nbcol!= nbcol-1)
                        {
                            if (cpt % nbcol == 0)
                            {
                                string s2 = ""; //+ '\u0009';
                                while (s2.Length !=9-s.Length)
                                {
                                    s2 = " "+s2;
                                }
                                pop += s2+s;
                                //pop += s + s2;
                            }
                            else if(cpt%nbcol ==1){
                                string s2 = ""; // + '\u0009';
                                while (s2.Length != 22 - s.Length)
                                {
                                    s2 = " " + s2;
                                }
                                pop += s2 + s;
                                //pop += s + s2;
                            }
                            else
                            {
                                string s2 = ""; // + '\u0009';
                                while (s2.Length != 16 - s.Length)
                                {
                                    s2 = " " + s2;
                                }
                                pop += s2 + s;
                                //pop += s + s2;
                            }
                            pop = pop.Replace("\n", "");
                            System.Diagnostics.Debug.WriteLine("ok2");
                        }
                        else
                        {
                            string s2 = ""; //+ '\u0009';
                            while (s2.Length != 16+2 - s.Length)
                            {
                                s2 = " " + s2;
                            }
                            pop += s2 + s;
                            //pop += s;
                            res.Add(pop);
                            pop = "";
                            System.Diagnostics.Debug.WriteLine("ok3");
                        }
                        cpt++;
                    }
                }

                string result = "";
                foreach(string r in res)
                {
                    result += r;
                }

                System.Diagnostics.Debug.WriteLine("ok4");
                File.WriteAllText(fil, result);

            }
        }
    }
}
