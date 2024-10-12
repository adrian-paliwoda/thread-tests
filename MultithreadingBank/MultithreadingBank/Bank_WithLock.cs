using System;
using System.Threading;

namespace MultithreadingBank
{
    public class Bank_WithLock : Bank
    {
        public Bank_WithLock()
        {
            BankAccounts = new BankAccountWithLock[SimulationParameters.NumberOfAccounts];

            RandomAccountIndices = new Random((int) DateTime.Now.Ticks);
            RandomTransferAmounts =
                new Random((int) DateTime.Now.Ticks + SimulationParameters.SomeNumberForRandom);
        }

        protected override BankAccount CreateNewBankAccount(int i, double initialDeposit)
        {
            return new BankAccountWithLock(i, SimulationParameters.InitialDeposit);
        }
    }
}