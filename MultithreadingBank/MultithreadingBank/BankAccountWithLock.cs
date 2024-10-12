using System;
using System.Threading;

namespace MultithreadingBank
{
    public class BankAccountWithLock : BankAccount
    {
        object lockObject = new object();

        public BankAccountWithLock(int acctNum, double initDeposit)
        {
            AccountNumber = acctNum;
            balance = initDeposit;
        }

        public override void AddAmount(double amount)
        {
            lock (lockObject)
            {
                double temp = balance;
                temp += amount;
                Thread.Sleep(1);
                balance = temp;
            }
        }

        public override void SubtractAmount(double amount)
        {
            AddAmount(-amount);
        }

        public override void TransferFrom(BankAccount otherAccount, double amount)
        {
            if (otherAccount is BankAccountWithLock accountWithLock)
            {
                TransferFrom(accountWithLock, amount);
            }
        }

        public new double Balance
        {
            get
            {
                double b = 0;

                lock (lockObject)
                {
                    b = balance;
                }

                return (b);
            }
        }

        public void TransferFrom(BankAccountWithLock otherAccount, double amount)
        {
            lock (this.lockObject)
            {
                Thread.Sleep(30);

                lock (otherAccount.lockObject)
                {
                    otherAccount.SubtractAmount(amount);
                    this.AddAmount(amount);
                }
            }

            Console.WriteLine("[{0}] Transfering {1:C0} from account {2} to {3}",
                Thread.CurrentThread.Name, amount,
                otherAccount.AccountNumber, this.AccountNumber);
        }
    }
}