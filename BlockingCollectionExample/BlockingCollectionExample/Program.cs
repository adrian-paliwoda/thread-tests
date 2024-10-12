using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BlockingCollectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
//            TakeFromBlockingCollection();
            TryTakeFromBlockingCollection();
        }

        public static void TakeFromBlockingCollection()
        {
            BlockingCollection<int> blockingCollection
                = new BlockingCollection<int>();

            var producer = Task.Run(() => { blockingCollection.CompleteAdding(); });

            var customer = Task.Run(() =>
            {
                var item = blockingCollection.Take();

                Console.WriteLine("Value of item is: {0}", item);
            });

            Task.WaitAll(producer, customer);
        }

        public static void TryTakeFromBlockingCollection()
        {
            BlockingCollection<int> blockingCollection
                = new BlockingCollection<int>();

            var producer = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    blockingCollection.Add(i);
                }

                blockingCollection.CompleteAdding();
            });

            var customer = Task.Run(() =>
            {
                while (!blockingCollection.IsCompleted)
                {
                    blockingCollection.TryTake(out int item);

                    Console.WriteLine("Value of item is: {0}", item);
                }
            });

            Task.WaitAll(producer, customer);
        }
    }
}