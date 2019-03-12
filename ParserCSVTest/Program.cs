using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserCSVTest
{
    public class Program
    {
        private void generateCSV()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    string s = generateCSV.GenerateLine();
            //    Console.WriteLine("Res: " + s);
            //}
        }
        private static void version1()
        {
            string myPath = Environment.CurrentDirectory;
            using (FileCSV csv = new FileCSV(myPath, "data.csv"))
            {
                int rows = (int)Math.Pow(10, 5);
                int progress = 40;
                csv.DeleteFile();
                csv.Write2File(rows, progress);
                csv.ReadFile2DB(rows, progress);
            }
        }

        public static void Main(string[] args)
        {

            //Program.version1();
            
            Console.ReadLine();
        }
    }
}
