using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using MultithreadingImageProcessing;

namespace ExecutionContextCompare
{
    [MemoryDiagnoser]
    public class ExecutionContextExample
    {
        [Params(2,4,8,16,32,64,128,256,512,1024,2048)]
        public int numberOfThreads { get; set; }

        private const int max = 100000;
        
        
        public ExecutionContextExample()
        {
        }

        [Benchmark]
        public void RunTaskWithExecutionContext()
        {
            var tasks = new Task[numberOfThreads];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => SomeWorkToDo());
            }

            Task.WaitAll(tasks);
        }
        
        [Benchmark]
        public void RunTaskWithoutExecutionContext()
        {
            ExecutionContext.SuppressFlow();
            var tasks = new Task[numberOfThreads];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => SomeWorkToDo());
            }

            Task.WaitAll(tasks);
            ExecutionContext.RestoreFlow();
        }

        private void SomeWorkToDo()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent?.FullName;
            
            var path =
                string.Concat(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\..\..")), @"\images\nature.jpg");
            var pathNew =
                string.Concat(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\..\..")), @"\images\nature2_", Task.CurrentId , ".jpg");

            var bitmap = new Bitmap(Image.FromFile(path));
            var result = ImageProcessing.Dilation(bitmap, 3);

            result.Save(pathNew);
        }
        
    }
}