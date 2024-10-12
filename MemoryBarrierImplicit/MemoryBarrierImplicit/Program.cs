using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using BenchmarkDotNet.Running;

namespace MemoryBarrierImplicit
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MemoryBarrierExample>();

            Console.ReadKey();
        }
    }
}