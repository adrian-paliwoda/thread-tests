using System;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Running;

namespace MultithreadingCompareSingleAndMultiThreadSum
{
    class Program
    {
        static void Main(string[] args)
        {
            var summarySingleCore = BenchmarkRunner.Run<SingleCoreSum>();
            var summaryMultiCore1 = BenchmarkRunner.Run<MultiCoreSum>();
            var summaryMultiCore = BenchmarkRunner.Run<MultiCoreSumByTask>();
        }
    }
}