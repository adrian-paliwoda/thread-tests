using System;
using BenchmarkDotNet.Running;

namespace MultithreadingLinqAndPlinqCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            var summaryLinq = BenchmarkRunner.Run<LinqComputer>();
            var summaryPlinq = BenchmarkRunner.Run<PlinqComputer>();
            
            Console.ReadLine();
        }
    }
}