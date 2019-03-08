using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParserCSVTest
{

    public class GenerateCSV
    {
        private Random r = new Random();
        private string[] Names = { "MNO", "SQL", "NET", "PLN" };

        private string getType()
        {
            int i = r.Next(Names.Length);
            return Names[i];
        }

        private int getValue(int v)
        {
            return r.Next(v);
        }

        private Double getDouble(int v)
        {
            return (Double)r.NextDouble() * v;
        }

        private long getLong(long l)
        {
            return Convert.ToInt64(r.NextDouble() * l);
        }

        //MNO,3,813496,36,30000,78.19,,
        public string GenerateLine()
        {

            string s = string.Format("{0},{1},{2},{3},{4},{5},,",
                   getType(),
                   getValue(10),
                   getLong((long)Math.Pow(1000, 3)),
                   getValue(100),
                   getValue((int)Math.Pow(2, 16)),
                   getDouble(100).ToString("F2").Replace(',', '.')
                   );
            return s;
        }
    }

    public class FileCSV
    {
        private string myPath;
        private string myFn;

        public FileCSV(string path, string file)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception("Invalid path");
            }

            myPath = path;
            myFn = Path.Combine(path, file);
            this.DeleteFile();
        }
        public void DeleteFile()
        {
            try
            {
                if (File.Exists(myFn))
                    File.Delete(myFn);
            }
            catch (Exception ex)
            {
                throw new Exception ("Cannot delete file", ex);
            }
        }
        public void Write2File(long iter = 10, int progLength = 20)
        {
            Stopwatch t = new Stopwatch();
            GenerateCSV generateCSV = new GenerateCSV();

            t.Start();

            using (StreamWriter sw = new StreamWriter(myFn))
            {
                string line;
                long progress = iter / progLength;
                Console.Write("\nProg.: [" + new string(' ', progLength) + "]");
                Console.SetCursorPosition(Console.CursorLeft - progLength - 1, Console.CursorTop);

                for (int i = 1; i <= iter; i++)
                {
                    if (i % progress == 0)
                        Console.Write("*");
                    line = generateCSV.GenerateLine();
                    sw.WriteLine(line);
                }
                Console.WriteLine();
            }
            t.Stop();
            Console.WriteLine("Passed: {0}", t.Elapsed);
        }

        public void ReadFile2DB()
        {
            
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            string myPath = Environment.CurrentDirectory;
            FileCSV csv = new FileCSV(myPath, "Teste.csv");

            csv.Write2File((int)Math.Pow(10,7), 40);

            //for (int i = 0; i < 10; i++)
            //{
            //    string s = generateCSV.GenerateLine();
            //    Console.WriteLine("Res: " + s);
            //}
            
            Console.ReadLine();
        }
    }
}
