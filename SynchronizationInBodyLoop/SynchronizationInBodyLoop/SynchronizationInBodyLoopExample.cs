using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace SynchronizationInBodyLoop
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions,
        HardwareCounter.BranchInstructions)]
    public class SynchronizationInBodyLoopExample
    {
        [Params(10, 100, 1000, 10000, 100000, 1000000, 10000000)]
        public int maxIterations { get; set; }

        private object lock_1;

        public SynchronizationInBodyLoopExample()
        {
            lock_1 = new object();
        }
        [Benchmark]

        public void SynchronizationWithLock()
        {
            long sum = 0;

            Parallel.For(
                0,
                maxIterations,
                index =>
                {
                    lock (lock_1)
                    {
                        sum += index;
                    }
                });
        }
        [Benchmark]

        public void SynchronizationWithInterlcked()
        {
            long sum = 0;

            Parallel.For(0, maxIterations, () => 0, 
                (int i, ParallelLoopState loop, int subTotal) =>
                {
                    subTotal += i;
                    return subTotal;
                },x => Interlocked.Add(ref sum, x));
        }
    }
}