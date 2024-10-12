using System;
using System.Threading;

namespace MultithreadingBank
{
    public class BankAccountMutex : BankAccount
    {
        Mutex mutexLock = new Mutex();

        public BankAccountMutex(int accountNumber, double initDeposit)
        {
            AccountNumber = accountNumber;
            balance = initDeposit;
        }

        public override void AddAmount(double amount)
        {
            if (mutexLock.WaitOne())
            {
                try
                {
                    double temp = balance;
                    temp += amount;
                    Thread.Sleep(1);
                    balance = temp;
                }
                finally
                {
                    mutexLock.ReleaseMutex();
                }
            }
        }

        public override void SubtractAmount(double amount)
        {
            AddAmount(-amount);
        }

        public override void TransferFrom(BankAccount otherAccount, double amount)
        {
            if (otherAccount is BankAccountMutex accountMutex)
            {
                TransferFrom(accountMutex, amount);
            }
        }

        public new double Balance
        {
            get
            {
                double b = 0;

                if (mutexLock.WaitOne())
                {
                    try
                    {
                        b = balance;
                    }
                    finally
                    {
                        mutexLock.ReleaseMutex();
                    }
                }

                return (b);
            }
        }

        public void TransferFrom(BankAccountMutex otherAccountMutex, double amount)
        {
            Mutex[] locks = {this.mutexLock, otherAccountMutex.mutexLock};

            if (WaitHandle.WaitAll(locks))
            {
                try
                {
                    otherAccountMutex.SubtractAmount(amount);
                    this.AddAmount(amount);
                }
                finally
                {
                    foreach (Mutex m in locks)
                    {
                        m.ReleaseMutex();
                    }
                }
            }

            Console.WriteLine("[{0}] Transfering {1:C0} from account {2} to {3}",
                Thread.CurrentThread.Name, amount,
                otherAccountMutex.AccountNumber, this.AccountNumber);
        }
    }
}