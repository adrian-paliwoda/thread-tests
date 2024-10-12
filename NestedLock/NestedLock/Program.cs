namespace NestedLock
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new NestedLockExample();
            
            a.NestedLockRightOrder();
            a.NestedLockWrongOrder();
        }
    }
}