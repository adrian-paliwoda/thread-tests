using System;
using System.Threading;
using System.Threading.Tasks;

namespace NestedLock
{
    internal class NestedLockExample
    {
        private object lock_1 = new object();
        private object lock_2 = new object();

        public void NestedLockWrongOrder()
        {
            var task1 = Task.Run(() =>
            {
                lock (lock_1)
                {
                    Console.WriteLine("Enter to first lock");
                    Thread.Sleep(500);
                    lock (lock_2)
                    {
                        Console.WriteLine("Enter to second lock");
                    }
                }
            });
            
            var task2 = Task.Run(() =>
            {
                lock (lock_2)
                {
                    Console.WriteLine("Enter to second lock");
                    Thread.Sleep(500);
                    lock (lock_1)
                    {
                        Console.WriteLine("Enter to first lock");
                    }
                }
            });

            Task.WaitAll(task1, task2);
        }
        
        public void NestedLockRightOrder()
        {
            var task1 = Task.Run(() =>
            {
                lock (lock_1)
                {
                    Console.WriteLine("Enter to first lock");
                    Thread.Sleep(500);

                    lock (lock_2)
                    {
                        Console.WriteLine("Enter to second lock");
                    }
                }
            });
            
            var task2 = Task.Run(() =>
            {
                lock (lock_1)
                {
                    Console.WriteLine("Enter to first lock");
                    Thread.Sleep(500);

                    lock (lock_2)
                    {
                        Console.WriteLine("Enter to second lock");
                    }
                }
            });
            
            Task.WaitAll(task1, task2);

        }
    }
}