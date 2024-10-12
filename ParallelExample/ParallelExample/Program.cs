using System;
using BenchmarkDotNet.Running;

namespace ParallelExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ParallelComparer>();
            var summary2 = BenchmarkRunner.Run<ParallelLoopsComparer>();

            Console.ReadKey();
        }
    }
}