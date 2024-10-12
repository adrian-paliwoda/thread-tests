using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadId
{
    public class ThreadId
    {
        public void RunThrads()
        {
            int numberOfTreads = 10;
            var threads = new Thread[numberOfTreads];

            for (int i = 0; i < numberOfTreads; i++)
            {
                threads[i] = new Thread(ShowIds);
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        public void RunTasks()
        {
            int numberOfTaks = 10;
            var task = new Task[numberOfTaks];

            Console.WriteLine("Tasks are creating....");
            for (int i = 0; i < numberOfTaks; i++)
            {
//                task[i] = Task.Run(() => { Console.WriteLine(Task.CurrentId); });
                task[i] = Task.Run(() => { });
            }


            Console.WriteLine("Tasks have been created.");
            Console.WriteLine();
            Console.WriteLine("Getting number of tasks from the latest.");
            for (int i = numberOfTaks - 1; i >= 0; i--)
            {
                Console.WriteLine(task[i].Id);
            }
        }

        public void ShowIds()
        {
            int idManagedThreadId = Thread.CurrentThread.ManagedThreadId;
            int idCurrentManagmentThread = Environment.CurrentManagedThreadId;

            Console.WriteLine("id from ManagedThreadId : " + idManagedThreadId);
            Console.WriteLine("id from CurrentManagedThreadID : " + idCurrentManagmentThread);
            
            ProcessThreadCollection threadlist = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread thread in threadlist)
            {
                Console.WriteLine(thread.Id);
            }
        }
    }
}