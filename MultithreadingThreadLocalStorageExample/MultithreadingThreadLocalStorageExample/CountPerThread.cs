using System;
using System.Threading.Tasks;

namespace MultithreadingThreadLocalStorageExample
{
        public class CountPerThread
        {
            [ThreadStatic] private static int _count = 10;
            private int numberOfTasks = 4;
            public static int Count
            {
                get => _count;
            }

            public static void Increnent()
            {
                _count++;
            }

            public void RunExample()
            {
                var tasks = new Task[numberOfTasks];
                for (int inddex = 0; inddex < tasks.Length; inddex++)
                {
                    tasks[inddex] =Task.Run( () =>
                    {
                        _count--;
                        Console.WriteLine(_count);
                    }) ;
                }

                Task.WaitAll(tasks);
            }
            
        }
}