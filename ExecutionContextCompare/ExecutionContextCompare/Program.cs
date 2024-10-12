using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Running;

namespace ExecutionContextCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ExecutionContextExample>();

            Console.ReadKey();
        }
    }
}