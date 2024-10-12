using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadingSum
{
    class Program
    {
        private static int threadCount = 4;
        private static int MAX = 10;
        private static CountdownEvent countdownEvent = new CountdownEvent(threadCount);

        static void Main(string[] args)
        {
            Task2();

            Console.WriteLine();
        }

        private static void Task1()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("threads.txt"))
            {
                for (threadCount = 2; threadCount < 200000; threadCount *= 2)
                {
                    countdownEvent = new CountdownEvent(threadCount);

                    Stopwatch thredsTime = new Stopwatch();
                    thredsTime.Start();
                    ComputeSumThreads();
                    thredsTime.Stop();

                    Stopwatch poolTime = new Stopwatch();
                    poolTime.Start();
                    ComputeSumThreadPool();
                    poolTime.Stop();

                    Stopwatch taskTime = new Stopwatch();
                    taskTime.Start();
                    ComputeSumTask();
                    taskTime.Stop();

                    String result = String.Format("{0}\t{1}\t{2}\t{3}", threadCount, thredsTime.ElapsedMilliseconds,
                        poolTime.ElapsedMilliseconds, taskTime.ElapsedMilliseconds);

                    file.WriteLine(result);
                    //file.WriteLine(thredsTime.ElapsedMilliseconds);
                    //file.WriteLine(poolTime.ElapsedMilliseconds);
                    //file.WriteLine(taskTime.ElapsedMilliseconds);
                }
            }
        }


        private static void Task2()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("itearations.txt"))
            {
                for (MAX = 655360; MAX < 1000000000; MAX *= 2)
                {
                    countdownEvent = new CountdownEvent(threadCount);

                    Stopwatch thredsTime = new Stopwatch();
                    thredsTime.Start();
                    ComputeSumThreads();
                    thredsTime.Stop();

                    Stopwatch poolTime = new Stopwatch();
                    poolTime.Start();
                    ComputeSumThreadPool();
                    poolTime.Stop();

                    Stopwatch taskTime = new Stopwatch();
                    taskTime.Start();
                    ComputeSumTask();
                    taskTime.Stop();

                    String result = String.Format("{0}\t{1}\t{2}\t{3}", MAX, thredsTime.ElapsedMilliseconds,
                        poolTime.ElapsedMilliseconds, taskTime.ElapsedMilliseconds);

                    file.WriteLine(result);
                    //file.WriteLine(thredsTime.ElapsedMilliseconds);
                    //file.WriteLine(poolTime.ElapsedMilliseconds);
                    //file.WriteLine(taskTime.ElapsedMilliseconds);
                }
            }
        }

        static void ComputeSumThreads()
        {
            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(SumThread);
                threads[i].Start();
            }


            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
        }

        static void ComputeSumTask()
        {
            Task[] tasks = new Task[threadCount];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => SumThread());
            }

            Task.WaitAll(tasks);
        }

        static void ComputeSumThreadPool()
        {
            for (var i = 0; i < threadCount; i++)
            {
                ThreadPool.QueueUserWorkItem((state) => SumPool());
            }

            countdownEvent.Wait();
        }


        static void SumThread()
        {
            double sum = 0.0;

            for (int i = 0; i < MAX; i++)
            {
                sum += i;
            }
        }

        static void SumPool()
        {
            double sum = 0.0;

            for (int i = 0; i < MAX; i++)
            {
                sum += i;
            }

            countdownEvent.Signal();
        }
    }
}