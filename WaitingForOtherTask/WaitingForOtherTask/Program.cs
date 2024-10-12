using System;
using BenchmarkDotNet.Running;

namespace WaitingForOtherTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<WaitingForOtherTaskExample>();

            Console.ReadKey();
        }
    }
}