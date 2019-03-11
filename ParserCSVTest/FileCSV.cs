using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using ParserCSVTest.Libs;
using ParserCSVTest.Data;

namespace ParserCSVTest
{
    public class FileCSV : IFileCSV
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
                throw new Exception("Cannot delete file", ex);
            }
        }
        public void Write2File(long iterations = 10, int progLength = 20)
        {
            MyWatch t = new MyWatch();
            GenerateCSV generateCSV = new GenerateCSV();

            t.Start();

            using (StreamWriter sw = new StreamWriter(myFn))
            {
                string line;
                MyProgBar progressBar = new MyProgBar(iterations, progLength);
                progressBar.Init();

                for (int i = 1; i <= iterations; i++)
                {
                    progressBar.Step(i);
                    line = generateCSV.GenerateLine();
                    sw.WriteLine(line);
                }
            }
            t.StopAndPrint();

        }

        public void ReadFile2DB(long iterations = 10, int progressLength = 20) //to do multiple versions
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
                MyProgBar progressBar = new MyProgBar(iterations, progressLength);
                progressBar.Init();

                while ((line = sr.ReadLine()) != null)
                {
                    progressBar.Step(i++);
                    List<string> newDataRow = line.Split(',').Take(6).ToList();
                    newDataRow = newDataRow.Select(s => s.Replace('.', ',')).ToList(); //Double

                    DataRow dr = db.Tables[0].NewRow();
                    Cols.ForEach(x => dr[x.ToString()] = newDataRow[Cols.IndexOf(x.ToString())]);
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
}
