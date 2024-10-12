using BenchmarkDotNet.Running;

namespace ConsoleApp5
{
    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ThreadCreator>();
//            var summary = BenchmarkRunner.Run<ThreadStarter>();
        }
        
    }
}