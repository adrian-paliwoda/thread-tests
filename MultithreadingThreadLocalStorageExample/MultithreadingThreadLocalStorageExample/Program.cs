using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;

namespace MultithreadingThreadLocalStorageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var countPerThread = new CountPerThread();
            countPerThread.RunExample();
        }

        public void RunExample()
        {
            var counterDown = new CounterDown();
            var numberOfThreads = 4;

            var threads = new Thread[numberOfThreads];
            for (int inddex = 0; inddex < threads.Length; inddex++)
            {
                threads[inddex] = new Thread(counterDown.CountDown);
                threads[inddex].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
        
        public static string FormatListOfProcess(List<string> processNames)
        {
            var formatedString = new StringBuilder();

            foreach (var processName in processNames)
            {
                formatedString.AppendLine($"Name: {processName}");
            }

            return formatedString.ToString();
        }
    }
}