using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using NUnitTests;

namespace TestSpace
{
    class myClass1
    {

        public int DoSomething(int a)
        {
            a = 6;
            return a;
        }

        public void DoSomething2(string b)
        {
            Console.WriteLine(b);
        }
    }

    class myClass2
    {
        public void DoSomething3(double c)
        {
            int k = 3 + 2;
        }
    }
}
