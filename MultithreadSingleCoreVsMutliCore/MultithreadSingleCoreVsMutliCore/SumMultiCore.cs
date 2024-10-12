using System.Threading;

namespace MultithreadSingleCoreVsMutliCore
{
    class SumMultiCore : IExercise
    {
        private int _maxItarations;
        private int _numberOfThreads;
        
        public SumMultiCore(int maxItarations, int numberOfThreads)
        {
            _maxItarations = maxItarations;
            _numberOfThreads = numberOfThreads;
        }

        public override void Run()
        {
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
        }
        
        private void Sum()
        {
            double sum = 0.0;
            
            for (int i = 0; i < _maxItarations; i++)
            {
                sum += i;
            }
        }

        public override string ToString()
        {
            return "Sum Multi Core";
        }
    }
}