using System;
using BenchmarkDotNet.Running;

namespace TaskAndContinueWith
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TaskAndContinueWithExample>();

            Console.ReadKey();
        }
    }
}