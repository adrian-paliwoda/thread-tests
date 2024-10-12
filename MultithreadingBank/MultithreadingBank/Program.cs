using System;

namespace MultithreadingBank
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank_Mutex bank_Mutex = new Bank_Mutex();
            Bank_WithChooseLocks bank_WithChooseLocks = new Bank_WithChooseLocks();
            Bank_WithLock bank_WithLock = new Bank_WithLock();
            Bank_WithoutLock bank_WithoutLock = new Bank_WithoutLock();

//          bank_WithoutLock.Simulation();
//          bank_WithLock.Simulation();
//          bank_WithChooseLocks.Simulation();
//          bank_Mutex.Simulation();
        }
    }
}