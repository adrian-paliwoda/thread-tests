using System;
using System.Threading;

namespace MultithreadSingleCoreVsMutliCore
{
    class ConsoleWriteMultiCore : IExercise
    {
        private int _maxIterations;
        private int _numberOfThreads;

        public ConsoleWriteMultiCore(int maxIterations, int numberOfThreads)
        {
            _maxIterations = maxIterations;
            _numberOfThreads = numberOfThreads;
        }

        public override void Run()
        {
            ThreadStart threadStart = ConoleWriete;
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
        

        public void ConoleWriete()
        {
            for (int i = 0; i < _maxIterations; i++)
            {
                Console.WriteLine("Hello, world!");
            }
        }

        public override string ToString()
        {
            return "Console WrietLine Multi Core";
        }
    }
}