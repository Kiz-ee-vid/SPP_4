using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestsGenerator
{
    public class Parallel
    {
        private readonly PipelineConfiguration PipelineConfig;

        public Parallel(PipelineConfiguration pipelineConfig)
        {
            PipelineConfig = pipelineConfig;
        }

        public async Task Execute(List<string> filesPath, string outputPath)
        {
            var readingBlock = new TransformBlock<string, string>(
                async filePath =>
                {
                    Console.WriteLine("File path:" + filePath);
                    using (StreamReader streamReader = new StreamReader(filePath))
                    {
                        return await streamReader.ReadToEndAsync();
                    }
                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = PipelineConfig.MaxReadingTasks } 
            );

            var generateTestClass = new TransformManyBlock<string, TestStructure>(
                async Code =>
                {
                    Console.WriteLine("Generating tests... ");
                    return await TestCreator.Generate(Code);
                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = PipelineConfig.MaxProcessingTasks }
            );

            var writeGeneratedFile = new ActionBlock<TestStructure>(
                async testClass =>
                {
                    string fullpath = Path.Combine(outputPath, testClass.TestName);
                    Console.WriteLine("Fullpath " + fullpath);
                    using (StreamWriter streamWriter = new StreamWriter(fullpath))
                    {
                        await streamWriter.WriteAsync(testClass.TestCode);
                    }

                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = PipelineConfig.MaxWritingTasks }
            );

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            readingBlock.LinkTo(generateTestClass, linkOptions);
            generateTestClass.LinkTo(writeGeneratedFile, linkOptions);

            foreach (string path in filesPath)
            {
                readingBlock.Post(path);
            }

            readingBlock.Complete();

            await writeGeneratedFile.Completion;
        }
    }
}
