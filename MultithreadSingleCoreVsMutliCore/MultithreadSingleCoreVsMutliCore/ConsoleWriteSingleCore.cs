using System;
using System.Threading;

namespace MultithreadSingleCoreVsMutliCore
{
    class ConsoleWriteSingleCore : IExercise
    {
        private int _maxItarations;
        private int _numberOfThreads;


        public ConsoleWriteSingleCore(int maxItarations, int numberOfThreads)
        {
            _maxItarations = maxItarations;
            _numberOfThreads = numberOfThreads;
        }
            
                
        public override void Run()
        {
            Thread.BeginThreadAffinity();

            ThreadStart threadStart = ConsoleWriteLine;
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
        private void ConsoleWriteLine()
        {
            Thread.BeginThreadAffinity();
            
            for (int i = 0; i < _maxItarations; i++)
            {
                Console.WriteLine("Hello, world!");
            }
            
            Thread.EndThreadAffinity();
        }

        public override string ToString()
        {
            return "Console WriteLine Single Core";
        }
    }
}