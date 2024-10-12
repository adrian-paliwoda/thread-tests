using System.Diagnostics;
using System.Threading.Tasks;

namespace MultithreadSingleCoreVsMutliCore
{
    class SumMultiCoreByTask : IExercise
    {
        private int _maxItarations;
        private int _numberOfThreads;
        
        public SumMultiCoreByTask(int maxItarations, int numberOfThreads)
        {
            _maxItarations = maxItarations;
            _numberOfThreads = numberOfThreads;
        }

        public override void Run()
        {
    
            var threads = new Task[_numberOfThreads];
            
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = Task.Run(Sum);
            }

            Task.WaitAll(threads);
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
            return "Sum Multi Core By Task";
        }
    }
}