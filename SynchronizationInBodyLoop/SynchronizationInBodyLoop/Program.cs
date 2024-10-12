using System;
using BenchmarkDotNet.Running;

namespace SynchronizationInBodyLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SynchronizationInBodyLoopExample>();

            Console.ReadKey();
        }
    }
}