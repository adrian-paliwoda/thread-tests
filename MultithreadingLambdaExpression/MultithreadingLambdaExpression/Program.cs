using System;
using BenchmarkDotNet.Running;

namespace MultithreadingLambdaExpression
{
    class Program
    {
        static void Main(string[] args)
        {
//            var summary = BenchmarkRunner.Run<ThreadByLambdaExpression>();
            var summary = BenchmarkRunner.Run<ThreadWithoutLambdaExpression>();

        }
    }
}