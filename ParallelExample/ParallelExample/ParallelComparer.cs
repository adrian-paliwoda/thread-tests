using System;
using System.Drawing;
using System.IO;
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
    public class ParallelComparer
    {
        [Params(2, 4, 8, 16, 32, 64, 128)]

        public int numberOfTasks { get; set; }

        [Benchmark]
        public void RunTask()
        {
            var tasks = new Task[numberOfTasks];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => LongTask());
            }

            Task.WaitAll(tasks);
        }

        [Benchmark]
        public void RunParallelInvoke()
        {
            var actions = new Action[numberOfTasks];
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = () => LongTask();
            }

            Parallel.Invoke(actions);
        }

        public void LongTask()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent?.FullName;

            var path =
                string.Concat(
                    Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\..\..")),
                    @"\images\n.jpg");
//            var pathNew =
//                string.Concat(
//                    Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\..\..")),
//                    @"\images\n2_", Task.CurrentId, ".jpg");

            var bitmap = new Bitmap(Image.FromFile(path));
            var result = ImageProcessing.Dilation(bitmap, 3);

            //result.Save(pathNew);
        }
    }
}