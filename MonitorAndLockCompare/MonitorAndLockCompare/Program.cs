using System;
using BenchmarkDotNet.Running;

namespace MonitorAndLockCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MonitorAndLockExamples>();

            Console.ReadKey();
        }
    }
}
