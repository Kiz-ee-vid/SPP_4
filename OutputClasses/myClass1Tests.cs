using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using TestSpace;

namespace TestSpace.Test
{
    [TestFixture]
    public class myClass1Tests
    {
        [Test]
        public void DoSomethingTest()
        {
            Assert.Fail("auto");
        }

        [Test]
        public void DoSomething2Test()
        {
            Assert.Fail("auto");
        }
    }
}