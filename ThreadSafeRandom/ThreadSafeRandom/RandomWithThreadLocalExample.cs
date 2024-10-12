using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace ThreadSafeRandom
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions,
        HardwareCounter.BranchInstructions)]
    public class RandomWithThreadLocalExample
    {
        [Params(2,4,8,16,32,64,128)]

        public int numberOfTasks { get; set; }

        private ThreadLocal<Random> random;
        private int maxNumbers;

        public RandomWithThreadLocalExample()
        {
            maxNumbers = 10000;
            random = new ThreadLocal<Random>(() => new Random());
        }
        [Benchmark]

        public void RunTasks()
        {
            var tasks = new Task[numberOfTasks];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => GenerateRandomNumber());
            }

            Task.WaitAll(tasks);
        }

        public void GenerateRandomNumber()
        {
            for (int i = 0; i < maxNumbers; i++)
            {
                int randomDigit = random.Value.Next(1, 10);
            }
        }
    }
}