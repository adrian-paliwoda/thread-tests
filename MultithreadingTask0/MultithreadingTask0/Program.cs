
using System;
using System.Threading;

namespace MultithreadingTask0
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("[{0}] processor/core count = {1} ", Thread.CurrentThread.ManagedThreadId, Environment.ProcessorCount);
        
            Thread thread = new Thread(SayHello);
            thread.Name = "Hello World";
            thread.Start();
        
            Console.WriteLine("[{0}] Main done", Thread.CurrentThread.ManagedThreadId);
    
        }
    
        static void SayHello()
        {
            Console.WriteLine("[{0}] Hello, World!", Thread.CurrentThread.ManagedThreadId);
        }

        static void DisplayMessage(object stateArg)
        {
            if (stateArg is string massage)
            {
                Console.WriteLine(massage);
            }
        }
    }
}
