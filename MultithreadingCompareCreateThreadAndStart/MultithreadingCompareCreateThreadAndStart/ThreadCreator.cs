using System;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace ConsoleApp5
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions, 
        HardwareCounter.BranchInstructions)]
    public class ThreadCreator
    {
        [Params(2, 4, 8, 16, 32, 64, 128)] 
        public int NumberOfThreads { get; set; }

        public ThreadCreator()
        {
        }

        [Benchmark]
        public void CreateThread()
        {
            var threads = new Thread[NumberOfThreads];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(SimpleMethod);
            }
        }

        public void SimpleMethod()
        {
            int max = 100;

            for (int i = 0; i < max; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}