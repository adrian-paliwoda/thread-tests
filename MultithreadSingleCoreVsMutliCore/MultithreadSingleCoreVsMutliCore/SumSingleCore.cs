using System.Threading;

namespace MultithreadSingleCoreVsMutliCore
{
    class SumSingleCore : IExercise
    {
        private int _maxItarations;
        private int _numberOfThreads;


        public SumSingleCore(int maxItarations, int numberOfThreads)
        {
            _maxItarations = maxItarations;
            _numberOfThreads = numberOfThreads;
        }

        public override void Run()
        {
            Thread.BeginThreadAffinity();

            ThreadStart threadStart = Sum;
            var threads = new Thread[_numberOfThreads];
            
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(threadStart);
                threads[i].Start();
            }
            
            foreach (var thread in threads)
            {
                thread.Join();
            }
            
            Thread.EndThreadAffinity();

        }

        private void Sum()
        {
            Thread.BeginThreadAffinity();

            double sum = 0.0;
            
            for (int i = 0; i < _maxItarations; i++)
            {
                sum += i;
            }

            Thread.EndThreadAffinity();
        }

        public override string ToString()
        {
            return "Sum Single Core";
        }
    }
}