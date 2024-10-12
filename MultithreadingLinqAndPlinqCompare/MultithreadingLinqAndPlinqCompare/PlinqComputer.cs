using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace MultithreadingLinqAndPlinqCompare
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions, 
        HardwareCounter.BranchInstructions)]
    public class PlinqComputer : ITaskContainer
    {
        [Params(10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000)]
        public int sizeOfData { get; set; }
        [Benchmark]
        public void SearchByWhere()
        {
            var generator = new GeneratorIEnumerable();
            var list = generator.GenertateIntList(sizeOfData);

            var result = list.AsParallel().Where(p => p % 2 == 0);
        }

        [Benchmark]
        public void SelectExample()
        {
            var generator = new GeneratorIEnumerable();
            List<int> list = generator.GenertateIntList(sizeOfData);
            
            double result = list.AsParallel()
                .Select(p => p * 9)
                .Select(p => Math.Sin((4 * Math.PI * p) / 900))
                .Select(p => Math.Pow(p, 5))
                .Sum();;
        }
    }
}