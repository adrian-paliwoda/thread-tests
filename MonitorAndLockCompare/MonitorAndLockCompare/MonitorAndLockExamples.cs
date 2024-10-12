using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace MonitorAndLockCompare
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions,
        HardwareCounter.BranchInstructions)]
    public class MonitorAndLockExamples
    {
        [Params(2, 4, 8, 16, 32, 64, 128, 256, 512, 1024)]
        public int numberOfThreads { get; set; }

        private const int max = 1000;
        private Object synchObjec = new Object();

        public int Sum { get; set; }

        public MonitorAndLockExamples()
        {
            Sum = 0;
        }

        [Benchmark]
        public void RunLock()
        {
            var tasks = new Task[numberOfThreads];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(LockExample);
            }


            Task.WaitAll(tasks);
        }

        private Task LockExample()
        {
            lock (synchObjec)
            {
                for (int j = 0; j < max; j++)
                {
                    Sum += j;
                }
                Console.WriteLine("costam");

            }

            return Task.FromResult<object>(null);
        }

        [Benchmark]
        public void RunMonitor()
        {
            var tasks = new Task[numberOfThreads];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(MonitorExample);
            }


            Task.WaitAll(tasks);
        }

        private Task MonitorExample()
        {
            Monitor.Enter(synchObjec);
            for (int i = 0; i < max; i++)
            {
                Sum += i;
            }
            Console.WriteLine("costam");

            Monitor.Exit(synchObjec);

            return Task.FromResult<object>(null);
        }
    }
}