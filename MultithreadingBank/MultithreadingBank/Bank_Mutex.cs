using System;
using System.Threading;

namespace MultithreadingBank
{
    public class Bank_Mutex : Bank
    {
        public Bank_Mutex()
        {
            BankAccounts = new BankAccountMutex[SimulationParameters.NumberOfAccounts];

            RandomAccountIndices = new Random((int) DateTime.Now.Ticks);
            RandomTransferAmounts =
                new Random((int) DateTime.Now.Ticks + SimulationParameters.SomeNumberForRandom);
        }

        protected override BankAccount CreateNewBankAccount(int i, double initialDeposit)
        {
            return new BankAccountMutex(i, SimulationParameters.InitialDeposit);
        }
    }
}