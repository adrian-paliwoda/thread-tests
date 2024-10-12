using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace TaskAndContinueWith
{
    [MemoryDiagnoser]
    public class TaskAndContinueWithExample
    {
        [Params(2, 4, 8, 16, 32, 64, 128, 256)]
        public int numberOfTask { get; set; }
        private int maxIterations;

        public TaskAndContinueWithExample()
        {
            maxIterations = 1000000000;
        }

        [Benchmark]
        public void RunManyTasks()
        {
            var tasks = new Task[numberOfTask];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => DoWork());
            }

            Task.WaitAll(tasks);
        }

        [Benchmark]
        public void RunContinueWith()
        {
            var master = Task.Run(() => DoWork());
            
            var tasks = new Task[numberOfTask - 1];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = master.ContinueWith((state) => DoWork());
            }

            master.Wait();
            Task.WaitAll(tasks);
        }

        private void DoWork()
        {
            int sum = 0;
            for (int i = 0; i < maxIterations; i++)
            {
                sum += i;
            }
        }
    }
}