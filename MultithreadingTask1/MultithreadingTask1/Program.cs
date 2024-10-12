using System;
using System.Threading;

namespace MultithreadingTask1
{
    //Mozna sprawdzic zuzycie CPU w przypadku uzycia Join i IsAlive
    class Program
    {
        private static volatile bool cancel = false;
        
        static void Main(string[] args)
        {
            UseJoin();
        }

        private static void UseJoin()
        {
            Thread thread = new Thread(SayHello);
            thread.Start();

            Console.WriteLine("Press Enter to cancel.");
            Console.ReadLine();

            cancel = true;

            thread.Join();
        }

        private static void UseIsAlive()
        {
            Thread thread = new Thread(SayHello);
            thread.Start();

            Console.WriteLine("Press Enter to cancel.");
            Console.ReadLine();

            cancel = true;

            while (thread.IsAlive)
            {
                thread.Join();
            }
        }

        static void SayHello()
        {
            while (!cancel)
            {
                Console.WriteLine("Hello, world!");
                Thread.Sleep(1000);
            }
        }
    }
}