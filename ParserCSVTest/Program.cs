using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserCSVTest
{

    public class Program
    {
        public static void Main(string[] args)
        {
            string myPath = Environment.CurrentDirectory;
            IFileCSV csv = new FileCSV(myPath, "Teste.csv");
            int rows = (int)Math.Pow(10, 5);
            int progress = 40;

            csv.Write2File(rows, progress);

            //for (int i = 0; i < 10; i++)
            //{
            //    string s = generateCSV.GenerateLine();
            //    Console.WriteLine("Res: " + s);
            //}

            csv.ReadFile2DB(rows, progress);

            Console.ReadLine();
        }
    }
}
