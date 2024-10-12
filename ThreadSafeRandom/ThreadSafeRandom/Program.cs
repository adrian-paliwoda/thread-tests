using System;
using BenchmarkDotNet.Running;

namespace ThreadSafeRandom
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary1 = BenchmarkRunner.Run<RandomWithThreadLocalExample>();
            var summary2 = BenchmarkRunner.Run<RandomWithLockExample>();
            
            Console.ReadKey();
        }
    }
}