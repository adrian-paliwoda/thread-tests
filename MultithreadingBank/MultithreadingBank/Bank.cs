using System;
using System.Threading;

namespace MultithreadingBank
{
    public abstract class Bank
    {
        protected BankAccount[] BankAccounts;

        protected Random RandomAccountIndices;

        protected Random RandomTransferAmounts;

        public void Simulation()
        {
            Thread.CurrentThread.Name = "Main thread";

            Thread[] transferThreads = new Thread[SimulationParameters.NumberOfTransferThreads];
            ThreadStart threadProc = new ThreadStart(TransferThreadProc);

            for (int i = 0; i < SimulationParameters.NumberOfAccounts; i++)
            {
                BankAccounts[i] = CreateNewBankAccount(i, SimulationParameters.InitialDeposit);
            }

            for (int n = 0; n < SimulationParameters.NumberOfTransferThreads; n++)
            {
                transferThreads[n] = new Thread(threadProc) {Name = $"Thread-{n}"};
                transferThreads[n].Start();
            }

            Console.WriteLine("[Main thread] Starting program. Total amount on deposit should always be {0:C0}",
                SimulationParameters.NumberOfAccounts * SimulationParameters.InitialDeposit);

            for (int n = 0; n < transferThreads.Length; n++)
            {
                transferThreads[n].Join();
            }

            Console.WriteLine("[Main thread] Simulation complete, Checking accounts.");
            CheckAmountIsCorrect();
        }

        protected void TransferThreadProc()
        {
            string threadName = Thread.CurrentThread.Name;

            int accountIndexToTransferTo = GetRandomAccountIndex();
            for (int i = 0; i < SimulationParameters.NumberOfIterations; i++)
            {
                double transferAmount = GetRandomTransferAmount();

                int accountIndexToTransferFrom = GetRandomAccountIndex();

                while (accountIndexToTransferTo == accountIndexToTransferFrom)
                {
                    accountIndexToTransferTo = GetRandomAccountIndex();
                }

                BankAccounts[accountIndexToTransferTo]
                    .TransferFrom(BankAccounts[accountIndexToTransferFrom], transferAmount);

                //Only for pretending work
                Thread.Sleep(SimulationParameters.TransferThreadPeriod);
            }
        }

        protected void CheckAmountIsCorrect()
        {
            string threadName = Thread.CurrentThread.Name;
            double totalDepositsIfNoErrors =
                SimulationParameters.InitialDeposit * SimulationParameters.NumberOfAccounts;
            double totalDeposits = 0;

            for (int n = 0; n < SimulationParameters.NumberOfAccounts; n++)
            {
                totalDeposits += BankAccounts[n].balance;
            }

            Console.WriteLine(
                Math.Abs(totalDeposits - totalDepositsIfNoErrors) < SimulationParameters.Precision
                    ? "[{0}] Checking amount: bank accounts are consistent ({1:C0} on deposit)"
                    : "[{0}] Checking amount: !inconsistencies detected! ({1:C0} total deposits)",
                threadName, totalDeposits);
        }

        private int GetRandomAccountIndex()
        {
            lock (RandomAccountIndices)
            {
                return RandomAccountIndices.Next(SimulationParameters.NumberOfAccounts - 1);
            }
        }


        public double GetRandomTransferAmount()
        {
            lock (RandomTransferAmounts)
            {
                return RandomTransferAmounts.Next((int) SimulationParameters.MinTransferAmount,
                    (int) SimulationParameters.MaxTransferAmount);
            }
        }

        protected abstract BankAccount CreateNewBankAccount(int i, double initialDeposit);
    }
}