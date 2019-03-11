using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ParserCSVTest.Libs
{
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
}
