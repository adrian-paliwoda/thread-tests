using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace MultithreadingCompareSingleAndMultiThreadSum
{
    [MemoryDiagnoser]
 
    public class MultiCoreSumByTask : IComputeSum
    {
        [Params(10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000)]

        public int SizeOfData { get;  set; }

        [Params(2, 4, 8, 16, 32, 64, 128, 256)]
        public int NumberOfThreads { get;  set; }
        
        public double Sum { get; private set; }

        private double[] _data;
        private int _numberOfDataForSingleThread;

        public MultiCoreSumByTask()
        {
            Sum = 0.0;
            _data = new double[SizeOfData];

            for (int i = 0; i < _data.Length; i++)
            {
                _data[i] = i;
            }
        }
        
        [Benchmark]
        public void ComputeSum()
        {
            _numberOfDataForSingleThread = (int)Math.Ceiling( 1.0 * _data.Length / NumberOfThreads);
            var tasks = new Task[NumberOfThreads];
            var sums = new double[tasks.Length];
            
            for (int i = 0; i < tasks.Length; i++)
            {
                var copyI = i;
                var start = i * _numberOfDataForSingleThread;
                var stop = start + _numberOfDataForSingleThread;

                sums[i] = 0.0;

                tasks[i] = Task.Run(() => GetSum(start, stop, ref sums[copyI]));
            }


            Task.WaitAll(tasks);

            foreach (var sum in sums)
            {
                Sum += sum;
            }

        }

        private void GetSum(int start, int stop, ref double sum)
        {
            for (int i = start; i < stop && i < _data.Length; i++)
            {
                sum += _data[i];
            }
        }
    }
        
        
}