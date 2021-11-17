using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSpace
{
    class myClass
    {

        public int DoSomething(int a)
        {
            a = 6;
            return a;
        }

        public void doSomethingPublic(string b)
        {
            Console.WriteLine(b);
        }
    }
}
