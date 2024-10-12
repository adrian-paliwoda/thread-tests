using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace MultithreadingCompareSingleAndMultiThreadSum
{
    [MemoryDiagnoser]

    public class SingleCoreSum : IComputeSum
    {
        [Params(10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000)]

        public int NumberOfDigitInArray { get; set; }
        
        public double Sum { get; private set; }
        
        private double[] _data;

        public SingleCoreSum()
        {
            _data = new double[NumberOfDigitInArray];
            Sum = 0.0;

            for (int index = 0; index < _data.Length; index++)
            {
                _data[index] = index;
            }
        }
        
        [Benchmark]
        public void ComputeSum()
        {
            foreach (var item in _data)
            {
                Sum += item;
            }
        }
    }
}