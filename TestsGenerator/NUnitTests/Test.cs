using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestsGenerator;

namespace NUnitTests
{
    [TestFixture]
    public class Tests
    {

        string fileCode;

        private List<TestStructure> generatedTests = null;

        private SyntaxNode TestClass;


        [SetUp]
        public void Setup()
        {
            string filePath = "../../../TestFile1.cs";

            fileCode = File.ReadAllText(filePath);

            generatedTests = TestCreator.Generate(fileCode).Result;

            TestClass = CSharpSyntaxTree.ParseText(generatedTests[0].TestCode).GetRoot();
        }

        [Test]
        public void AmountOfClassesInListTests()
        {
            Assert.AreEqual(generatedTests.Count,2);
        }

        [Test]
        public void AmountOfMethodsTests()
        {
            int methodsOneCount = TestClass.DescendantNodes().OfType<MethodDeclarationSyntax>().Count();
            Assert.AreEqual(2, methodsOneCount);
        }


        [Test]
        public void AmountOfClassesTest()
        {
            int classesOneCount = TestClass.DescendantNodes().OfType<ClassDeclarationSyntax>().Count();
            Assert.AreEqual(classesOneCount, 1);
        }

        [Test]
        public void AmountOfNamespacesTest()
        {
            int namespacesOneCount = TestClass.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Count();
            Assert.AreEqual(namespacesOneCount, 1);
        }


        [Test]
        public void NameOfTestFiles() 
        {
            Assert.AreEqual(generatedTests[0].TestName, "myClass1Tests.cs");
        }

        [Test]
        public void MethodsNamesTest()
        {
            MethodDeclarationSyntax method = TestClass.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            Assert.IsTrue(method.Identifier.ToString() == "DoSomethingTest");
        }

        [Test]
        public void MethodsBodyTest()
        {
            BlockSyntax methodBody = TestClass.DescendantNodes().OfType<MethodDeclarationSyntax>().First().Body;
            string  mb=methodBody.ToString();
            Assert.IsTrue(mb.Contains("Assert.Fail(\"auto\")"));

        }

        [Test]
        public void AttributesTest()
        {
            Assert.AreEqual(1, TestClass.DescendantNodes().OfType<ClassDeclarationSyntax>()
               .Where((classDeclaration) => classDeclaration.AttributeLists.Any((attributeList) => attributeList.Attributes
               .Any((attribute) => attribute.Name.ToString() == "TestFixture"))).Count());

            MethodDeclarationSyntax method = TestClass.DescendantNodes().OfType<MethodDeclarationSyntax>().First();

            Assert.IsTrue(method.AttributeLists.Any((attributeList) => attributeList.Attributes
                        .Any((attribute) => attribute.Name.ToString() == "Test")));
                
        }
    }
}