using System;
using System.Threading;

namespace MultithreadingBank
{
    public class Bank_WithoutLock : Bank
    {
        public Bank_WithoutLock()
        {
            BankAccounts = new BankAccountWithoutLock[SimulationParameters.NumberOfAccounts];

            RandomAccountIndices = new Random((int) DateTime.Now.Ticks);
            RandomTransferAmounts =
                new Random((int) DateTime.Now.Ticks + SimulationParameters.SomeNumberForRandom);
        }

        protected override BankAccount CreateNewBankAccount(int i, double initialDeposit)
        {
            return new BankAccountWithoutLock(i, SimulationParameters.InitialDeposit);
        }
    }
}