using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestsGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string FolderPath = "../../../../OutputClasses/";

            List<string> FilesPath = new List<string>() 
            {
                "../../../NUnitTests/TestFile1.cs",
           //    "../../../NUnitTests/TestFile2.cs"
            };

            Parallel p = new Parallel(new PipelineConfiguration(1, 1, 1));
            await p.Execute(FilesPath, FolderPath);
            Console.WriteLine("Classes Generated");
            Console.ReadLine();
        }
    }
}
