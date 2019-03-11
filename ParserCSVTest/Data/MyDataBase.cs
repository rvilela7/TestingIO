using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserCSVTest.Data
{
    public sealed class MyDataBase
    {
        public DBSet db;
        public MyDataBase()
        {
            db = new DBSet();
        }
    }
}
