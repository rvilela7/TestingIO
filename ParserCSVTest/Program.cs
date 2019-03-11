using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using ParserCSVTest.Data;
using System.Data;

namespace ParserCSVTest
{
    public class MyProgBar
    {
        private long iterations; //Ciclo
        private int stepDivider; //Divisor
        private long progress; 

        public MyProgBar(long iter, int progLength)
        {
            this.iterations = iter;
            this.stepDivider = progLength;
            this.progress = iter / progLength;
        }

        public void Init()
        { 
            Console.Write("\nProg.: [" + new string(' ', stepDivider) + "]");
            Console.SetCursorPosition(Console.CursorLeft - stepDivider - 1, Console.CursorTop);
        }

        public void Step(long i)
        {
            if (i % progress == 0)
                Console.Write("*");
        }
    }

    public sealed class MyDataBase
    {
        public DBSet db;
        public MyDataBase()
        {
            db = new DBSet();
        }
    }

    public class MyWatch
    {
        public Stopwatch myWatch;

        public MyWatch()
        {
            myWatch = new Stopwatch();
        }

        public void Start()
        {
            myWatch.Start();
        }

        public void Stop()
        {
            myWatch.Stop();
        }

        public void Print()
        {
            Console.WriteLine("\nPassed: {0}", myWatch.Elapsed);
        }

        public void StopAndPrint()
        {
            Stop();
            Print();
        }
    }


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
            MyWatch t = new MyWatch();
            GenerateCSV generateCSV = new GenerateCSV();

            t.Start();

            using (StreamWriter sw = new StreamWriter(myFn))
            {
                string line;
                MyProgBar progressBar = new MyProgBar( iter, progLength );
                progressBar.Init();

                for (int i = 1; i <= iter; i++)
                {
                    progressBar.Step(i);
                    line = generateCSV.GenerateLine();
                    sw.WriteLine(line);
                }
            }
            t.StopAndPrint();

        }

        public void ReadFile2DB(long iter = 10, int progLength = 20) //to do multiple versions
        {
            MyDataBase dtb = new MyDataBase();
            var db = dtb.db;
            var Cols = db.Tables[0].Columns.Cast<DataColumn>().Where(x => x.ColumnName != "id").Select(y => y.ColumnName).ToList();
            //var Cols = (from dc in db.DataTable1.Columns.Cast<DataColumn>()
            //            where dc.ColumnName != "id"
            //             select dc.ColumnName).ToList();

            //Cols.ForEach(Console.WriteLine);
            string line;

            MyWatch t = new MyWatch();

            using (StreamReader sr = new StreamReader(myFn))
            {
                int i = 1;
                MyProgBar progressBar = new MyProgBar(iter, progLength);
                progressBar.Init();

                while ((line = sr.ReadLine()) != null)
                {
                    progressBar.Step(i++);
                    List<string> newDataRow = line.Split(',').Take(6).ToList();
                    newDataRow = newDataRow.Select(s => s.Replace('.', ',')).ToList(); //Double

                    DataRow dr = db.Tables[0].NewRow();
                    Cols.ForEach( x => dr[x.ToString()] = newDataRow[Cols.IndexOf(x.ToString())] );
                    db.Tables[0].Rows.Add(dr);
                }
            }

            t.StopAndPrint();
        }

        public string ListDB()
        {
            //DBSet db = new DBSet();
            return "";
        }
}


    public class Program
    {
        public static void Main(string[] args)
        {
            string myPath = Environment.CurrentDirectory;
            FileCSV csv = new FileCSV(myPath, "Teste.csv");
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
