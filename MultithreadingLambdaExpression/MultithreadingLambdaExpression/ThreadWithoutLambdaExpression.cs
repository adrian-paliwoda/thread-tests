using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace MultithreadingLambdaExpression
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions, 
        HardwareCounter.BranchInstructions)]
    public class ThreadWithoutLambdaExpression
    {
        [Params(2,4,8,16,32,64,128,256)]
        public int numberOfThreads { get; set; }
        [Params(100,1000,10000,100000,1000000)]
        public int sumCount { get; set; }

        public ThreadWithoutLambdaExpression()
        {
                
        }

        [Benchmark]
        public void CreateThreadWithMethodWithoutParameter()
        {
            var threads = new Thread[numberOfThreads];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(MethodWithoutParameter);
                threads[i].Start();
            }
        }
        
        [Benchmark]
        public void CreateThreadWithMethodWithParameter()
        {
            var threads = new Thread[numberOfThreads];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(MethodWithParameter);
                threads[i].Start(sumCount);
            }
        }
        
        private void MethodWithoutParameter()
        {
            double sum = 0.0;

            for (int i = 0; i < sumCount; i++)
            {
                sum += i;
            }
        } 
        private void MethodWithParameter(object a_max)
        {
            int max = (int) a_max;
            double sum = 0.0;

            for (int i = 0; i < max; i++)
            {
                sum += i;
            }
        }
        
    }
}