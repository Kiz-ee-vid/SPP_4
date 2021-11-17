namespace TestsGenerator
{
    public class TestStructure
    {
        public string TestName;
        public string TestCode; 

        public TestStructure(string name, string content)
        {
            TestName = name + ".cs";
            TestCode = content;
        }
    }
}