using System;
using BenchmarkDotNet.Running;

namespace ClosuresExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ClosuresExamples>();

            Console.ReadKey();
        }
    }
}