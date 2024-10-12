using System;
using System.Threading.Tasks;

namespace LocalVariableEvaluation
{
    internal class LocalVariableEvaluationExample
    {
        public void LocalVariableEvaluationWrongValue()
        {
            int numberOfTasks = 10;
            var tasks = new Task[numberOfTasks];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    Console.WriteLine("For task with id {0} counter has value: {1}",
                        Task.CurrentId, i);
                });
            }

            Task.WaitAll(tasks);
        }

        public void LocalVariableEvaluationGoodValue()
        {
            int numberOfTasks = 10;
            for (int i = 0; i < numberOfTasks; i++)
            {
                int loopCounter = i;
                Task.Run(() =>
                {
                    Console.WriteLine("For task with id {0} counter has value: {1}",
                        Task.CurrentId, loopCounter);
                });
            }
        }

        public void LocalVariableEvaluationGoodValueWithFactoryStartNew()
        {
            int numberOfTasks = 10;
            for (int i = 0; i < numberOfTasks; i++)
            {
                Task.Factory.StartNew((stateObj) =>
                {
                    int loopCounter = (int) stateObj;
                    Console.WriteLine("For task with id {0} counter has value: {1}",
                        Task.CurrentId, loopCounter);
                }, i);
            }
        }
    }
}