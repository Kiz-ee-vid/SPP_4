using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using TestSpace;

namespace TestSpace.Test
{
    [TestFixture]
    public class myClassTests
    {
        [Test]
        public void DoSomethingTest()
        {
            Assert.Fail("auto");
        }

        [Test]
        public void doSomethingPublicTest()
        {
            Assert.Fail("auto");
        }
    }
}