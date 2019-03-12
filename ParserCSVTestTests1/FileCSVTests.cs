using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParserCSVTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                csv.touchFile();
                Assert.IsTrue(File.Exists(csv.path));
                csv.DeleteFile();
                Assert.IsFalse(File.Exists(csv.path));
            }
        }

        [Ignore]
        [TestMethod()]
        public void Write2FileTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void ReadFile2DBTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void touchFileTest()
        {
            using (FileCSV csv = new FileCSV(myPath, myFn))
            {
                csv.touchFile();
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