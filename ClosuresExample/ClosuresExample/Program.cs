using System;

namespace ClosuresExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var closureFactory = new ClosureFactory();
            
            closureFactory.ShowClosureInAction();
        }
    }

    class ClosureFactory
    {
        public void ShowClosureInAction()
        {
            Action function_1 = GetAction();
            Action function_2 = GetAction();

            Console.WriteLine("Calling function_1");
            function_1();
            function_1();
            function_1();
            Console.WriteLine();

            Console.WriteLine("Calling function_1");
            function_2();
            function_2();
            function_2();
        }


        public Action GetAction()
        {
            int i = 0;
            return () =>
            {
                Console.WriteLine(string.Format(
                        "Method called with i valued: {0}", i++
                    )
                );
            };
        }
    }
}