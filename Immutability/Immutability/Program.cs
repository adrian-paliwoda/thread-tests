using System;
using System.Threading;
using System.Threading.Tasks;

namespace Immutability
{
    class Program
    {
        static void Main(string[] args)
        {
            var mutability = new Mutability();
            mutability.ShowMutability();
        }
        
    }
}