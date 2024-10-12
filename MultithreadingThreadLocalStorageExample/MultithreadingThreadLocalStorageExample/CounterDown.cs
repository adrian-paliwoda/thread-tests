using System;
using System.Threading;

namespace MultithreadingThreadLocalStorageExample
{
    class CounterDown
    {
        [ThreadStatic] private static int _countStatic = 10;
        private static ThreadLocal<int> _countLocal = new ThreadLocal<int>(() => 10);

        public void CountDown()
        {
            _countStatic--;
            _countLocal.Value--;

            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}:\tThreadStatic: {_countStatic},\tThreadLocal: {_countLocal}");
        }
    }
}