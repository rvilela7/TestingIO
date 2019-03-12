using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCSVTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace ParserCSVTest.Tests
{
    [TestClass()]
    public class FileCSVTests
    {
        private string myPath;
        private string myFn;

        [TestInitialize]
        public void TestInit()
        {
            myPath = AppDomain.CurrentDomain.BaseDirectory;
            myFn = "TestFile.csv";
        }

        [Ignore]
        [TestMethod()]
        public void FileCSVTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteFileTest()
        {
            using (FileCSV csv = new FileCSV(myPath, myFn))
            {
                csv.TouchFile();
                Assert.IsTrue(File.Exists(csv.path));
                csv.DeleteFile();
                Assert.IsFalse(File.Exists(csv.path));
            }
        }

        [DataTestMethod]
        //Cannot test second parameter
        [DataRow( 10000 , 40)]
        [DataRow( 100 , 10)]
        public void Write2FileTest(int rows, int progress)
        {
            using (FileCSV csv = new FileCSV(myPath, myFn))
            {
                csv.Write2File(rows, progress);
                Assert.IsTrue(File.Exists(csv.path));
                String[] fl = File.ReadAllLines(csv.path);
                Assert.IsTrue(fl.Count() == rows);
                Random r = new Random();
                for (int i = 0; i < 10; i++)
                {
                    Assert.IsTrue(Regex.IsMatch(fl[r.Next(rows)], @"^[A-Z]+,\d,\d+,\d+,\d+,\d+\.\d.,,$")); //MNO,3,813496,36,30000,78.19
                }
                //File will be recreated on second datarow
            }
        }

        [Ignore]
        [TestMethod()]
        public void ReadFile2DBTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TouchFileTest()
        {
            using (FileCSV csv = new FileCSV(myPath, myFn))
            {
                csv.TouchFile();
                Assert.IsTrue(File.Exists(csv.path));
            }
        }

        [Ignore]
        [TestMethod()]
        public void ListDBTest()
        {
            Assert.Fail();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            string p = Path.Combine(myPath, myFn);
            if (File.Exists(Path.Combine(myPath, myFn)))
                File.Delete(p);
        }
    }
}