using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace MemoryBarrierImplicit
{
    [MemoryDiagnoser]
    public class MemoryBarrierExample
    {
        public int numberOfTasks { get; set; }
        [Params(1000000, 100000000, 100000000000)] public long sizeOfData { get; set; }

        public MemoryBarrierExample()
        {
            numberOfTasks = 8;
        }

        [Benchmark]
        public void SharedVariable()
        {
            var tasks = new Task[numberOfTasks];
            for (int i = 0; i < tasks.Length; i++)
            {
                long sum = 0;
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < sizeOfData; j++)
                    {
                        sum += i;
                    }
                });
            }
            Task.WaitAll(tasks);
        }

        [Benchmark]
        public void LocalVariable()
        {
            var tasks = new Task[numberOfTasks];
            for (int i = 0; i < tasks.Length; i++)
            {
                long sum = 0;
                tasks[i] = Task.Run(() =>
                {
                    long local_sum = 0;
                    for (int j = 0; j < sizeOfData; j++)
                    {
                        local_sum += i;
                    }

                    sum += local_sum;
                });
            }

            Task.WaitAll(tasks);
        }
    }
}