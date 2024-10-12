using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ReportingProgress
{
    class Program
    {
        static void Main(string[] args)
        {
            var progressExample = new ProgressExample();

            progressExample.CallReportMethod();
        }
    }

    class ProgressExample
    {
        public void DoSomething(IProgress<int> progress)
        {
            int sum = 0;
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                sum += i;
                progress.Report(sum);
            }
        }

        public void CallReportMethod()
        {
            var progress = new Progress<int>( report => Console.WriteLine(report));
            var task = Task.Run(() => DoSomething(progress));

            task.Wait();
        }
    }
}