using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace ClosuresExamples
{
    [MemoryDiagnoser]
    public class ClosuresExamples
    {
        [Params(2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048)]

        public int numberOfThreads { get; set; }

        [Params(10, 100, 1000, 10000, 100000)] public int sizeOfArray { get; set; }

        public ClosuresExamples()
        {
        }

        [Benchmark]
        public void RunClusores()
        {
            var arrayOfInts = new int[sizeOfArray];
            for (int i = 0; i < arrayOfInts.Length; i++)
            {
                arrayOfInts[i] = i;
            }

            for (int i = 0; i < numberOfThreads; i++)
            {
                Task.Run(() =>
                {
                    for (int j = 0; j < arrayOfInts.Length; j++)
                    {
                        arrayOfInts[i] += 1;
                    }
                });
            }
        }
    }
}