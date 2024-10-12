namespace MultithreadSingleCoreVsMutliCore
{
    class SumSingleCoreOneThread : IExercise
    {
        private int _maxItarations;
        private int _numberOfThreads;

        public SumSingleCoreOneThread(int maxItarations, int numberOfThreads)
        {
            _maxItarations = maxItarations;
            _numberOfThreads = numberOfThreads;
        }
        
        public override void Run()
        {
            Sum();
        }
        
        private void Sum()
        {
            double sum = 0.0;
            
            for (int i = 0; i < _maxItarations * _numberOfThreads; i++)
            {
                sum += i;
            }
        }
        
        public override string ToString()
        {
            return "Sum Single Core One Thread";
        }
        
    }
}