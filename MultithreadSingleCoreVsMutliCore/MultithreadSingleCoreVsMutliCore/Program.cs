using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultithreadSingleCoreVsMutliCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var iterations = 100000000;
            var numberOfThreads = 1000;
            
            var stopwatch= new Stopwatch();
            var exercises = new List<IExercise>();
            
//            exercises.Add(new ConsoleWriteSingleCore(iterations, numberOfThreads));
//            exercises.Add(new ConsoleWriteMultiCore(iterations, numberOfThreads));
            
            exercises.Add(new SumMultiCore(iterations, numberOfThreads));
            exercises.Add(new SumMultiCoreByTask(iterations, numberOfThreads));
            exercises.Add(new SumSingleCore(iterations, numberOfThreads));
            exercises.Add(new SumSingleCoreOneThread(iterations, numberOfThreads));


            foreach (var exercise in exercises)
            {
                stopwatch.Start();
                exercise.Run();
                stopwatch.Stop();
                
                Console.WriteLine(String.Concat(exercise.ToString() ,": ",stopwatch.ElapsedMilliseconds, " ms."));

                stopwatch.Reset();
            }
            
        }
    }
}