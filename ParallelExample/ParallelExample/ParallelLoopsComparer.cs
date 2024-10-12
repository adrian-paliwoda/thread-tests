using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using MultithreadingImageProcessing;

namespace ParallelExample
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions,
        HardwareCounter.BranchInstructions)]
    public class ParallelLoopsComparer
    {
        public int numberOfTasks { get; set; }

        [Benchmark]

        public void RunFor()
        {
            Parallel.For(0, numberOfTasks, LongTask);
        }

        [Benchmark]

        public void RunForeach()
        {
            List<int> integerList = Enumerable.Range(1, numberOfTasks).ToList();
            Parallel.ForEach(integerList, i => { LongTask(0); });


        }
        
        public void LongTask(int i )
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent?.FullName;
            
            var path =
                string.Concat(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\..\..")), @"\images\n.jpg");
            var pathNew =
                string.Concat(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\..\..")), @"\images\n2_", Task.CurrentId , ".jpg");

            var bitmap = new Bitmap(Image.FromFile(path));
            var result = ImageProcessing.Dilation(bitmap, 3);

            result.Save(pathNew);
        }
    }
}