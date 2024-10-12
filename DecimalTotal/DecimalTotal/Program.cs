using System;

namespace DecimalTotal
{
    class Program
    {
        static void Main(string[] args)
        {
            var decimalTotal = new DecimalTotal();
            decimalTotal.Add(10);
            Console.WriteLine(decimalTotal.Total);
        }
    }
}