using System;
using System.Threading;

namespace MultithreadingTask2
{
    class Program
    {
        static void Main(string[] args)
        {
            BackGroundThread();
        }

        static void ForeGroundThread()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);

            Thread thread = new Thread(SayHello);
            thread.Start(10);

            Console.WriteLine("[{0}] Main done.", Thread.CurrentThread.ManagedThreadId);
        }
        
        static void BackGroundThread()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);

            Thread thread = new Thread(SayHello);
            thread.IsBackground = true;
            thread.Start(10);

            thread.Join();
            Console.WriteLine("[{0}] Main done.", Thread.CurrentThread.ManagedThreadId);
        }
        
        static void SayHello(object arg)
        {
            int iterations = (int) arg;

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine("[{0}] Hello, world {1}! ({2})", Thread.CurrentThread.ManagedThreadId, i, Thread.CurrentThread.IsBackground);
            }
        }
    }
    
}