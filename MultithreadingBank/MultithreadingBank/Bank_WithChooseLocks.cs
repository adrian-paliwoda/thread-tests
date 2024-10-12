using System;
using System.Threading;

namespace MultithreadingBank
{
    public class Bank_WithChooseLocks : Bank
    {
        public Bank_WithChooseLocks()
        {
            BankAccounts = new BankAccountWithChooseLocks[SimulationParameters.NumberOfAccounts];

            RandomAccountIndices = new Random((int) DateTime.Now.Ticks);
            RandomTransferAmounts =
                new Random((int) DateTime.Now.Ticks + SimulationParameters.SomeNumberForRandom);
        }

        protected override BankAccount CreateNewBankAccount(int i, double initialDeposit)
        {
            return new BankAccountWithChooseLocks(i, SimulationParameters.InitialDeposit);
        }
    }
}