using System;
using System.Threading;

namespace MultithreadTask5
{
    class Program
    {
        static int sum = 0; //shared resource

        static void Main(string[] args)
        {
            Task2Interlocked();
        }

        public static void Task1Problem()
        {
            int threadsCount = 100;
            int iterations = 10000;

            Thread[] threads = new Thread[threadsCount];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(IncrementValue);
                threads[i].Start(iterations);
            }


            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine(sum);
            Console.WriteLine("[{0}] sum: {1}", Thread.CurrentThread.ManagedThreadId, sum);
            Console.WriteLine(sum);

        }

        public static void Task2Interlocked()
        {
            int threadsCount = 100;
            int iterations = 10000;

            Thread[] threads = new Thread[threadsCount];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(InterlockedIncrementValue);
                threads[i].Start(iterations);
            }


            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine(sum);
            Console.WriteLine("[{0}] sum: {1}", Thread.CurrentThread.ManagedThreadId, sum);
            Console.WriteLine(sum);

        }

        static void IncrementValue(object args)
        {
            int interations = (int)args;

            for (int i = 0; i < interations; i++)
            {
                sum++;//critical section
            }

            Console.WriteLine("[{0}] IncrementValue called", Thread.CurrentThread.ManagedThreadId);
        }

        static void InterlockedIncrementValue(object args)
        {
            int interations = (int)args;

            for (int i = 0; i < interations; i++)
            {
                Interlocked.Increment(ref sum);
            }

            Console.WriteLine("[{0}] InterlockedIncrementValue called", Thread.CurrentThread.ManagedThreadId);
        }

    }
}
