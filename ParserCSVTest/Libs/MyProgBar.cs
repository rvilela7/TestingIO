using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCSVTest.Libs
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
            try {
                Console.SetCursorPosition(Console.CursorLeft - stepDivider - 1, Console.CursorTop);
            }
            catch (System.IO.IOException ex)
            {
                //TestUnit issue due to CursorLeft/CursorTop
            }
        }

        public void Step(long i)
        {
            if (i % progress == 0)
                Console.Write("*");
        }
    }
}
