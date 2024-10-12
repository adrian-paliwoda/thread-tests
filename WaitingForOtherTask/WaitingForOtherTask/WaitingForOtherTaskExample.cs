using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;

namespace WaitingForOtherTask
{
    [MemoryDiagnoser]
    [HardwareCounters(
        HardwareCounter.BranchMispredictions,
        HardwareCounter.BranchInstructions)]
    public class WaitingForOtherTaskExample
    {
        [Params(2, 4, 8, 16, 32, 64)] public int numberOfWaitingTasks { get; set; }

        public void WaitForOneTask()
        {
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();

            var workingTask = Task.Run(() =>
            {
                cancellationTokenSource.Token.WaitHandle.WaitOne();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }, cancellationTokenSource.Token);

            var waitingTaskWithLoop = Task.Run(() =>
            {
                while (!workingTask.Status.HasFlag(TaskStatus.Canceled))
                {
                }
            });

            var waitingTaskSpinWait = Task.Run(() =>
            {
                while (!workingTask.Status.HasFlag(TaskStatus.Canceled))
                {
                    Thread.SpinWait(100);
                }
            });

            cancellationTokenSource.Cancel();
        }

        [Benchmark]
        public void WaitForOneTaskWithWaitSpin()
        {
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();

            var workingTask = Task.Run(() =>
            {
                cancellationTokenSource.Token.WaitHandle.WaitOne();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }, cancellationTokenSource.Token);

            var waitingTasksWithSpinWait = new Task[numberOfWaitingTasks];

            for (int i = 0; i < waitingTasksWithSpinWait.Length; i++)
            {
                waitingTasksWithSpinWait[i] = Task.Run(() =>
                {
                    while (!workingTask.Status.HasFlag(TaskStatus.Canceled))
                    {
                        Thread.SpinWait(1000);
                    }
                });
            }

            cancellationTokenSource.Cancel();
            Task.WaitAll(waitingTasksWithSpinWait);
        }

        [Benchmark]
        public void WaitForOneTaskWithLoop()
        {
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource();

            var workingTask = Task.Run(() =>
            {
                cancellationTokenSource.Token.WaitHandle.WaitOne();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            }, cancellationTokenSource.Token);

            numberOfWaitingTasks = 4;
            var waitingTasksWithLoop = new Task[numberOfWaitingTasks];

            for (int i = 0; i < waitingTasksWithLoop.Length; i++)
            {
                waitingTasksWithLoop[i] = Task.Run(() =>
                {
                    while (!workingTask.Status.HasFlag(TaskStatus.Canceled))
                    {
                    }
                });
            }

            cancellationTokenSource.Cancel();
            Task.WaitAll(waitingTasksWithLoop);
        }
    }
}