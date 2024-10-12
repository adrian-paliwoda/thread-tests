using System;
using System.Threading;

namespace MultithreadingBank
{
    public class BankAccountWithChooseLocks : BankAccount
    {
        object lockObject = new object();

        public BankAccountWithChooseLocks(int accountNumber, double initDeposit)
        {
            AccountNumber = accountNumber;
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
            if (otherAccount is BankAccountWithChooseLocks accountWithChooseLocks)
            {
                TransferFrom(accountWithChooseLocks, amount);
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

        public void TransferFrom(BankAccountWithChooseLocks otherAccount, double amount)
        {
            object firstLock;
            object secondLock;

            ChooseLocks(this, otherAccount, out firstLock, out secondLock);

            lock (firstLock)
            {
                Thread.Sleep(10);

                lock (secondLock)
                {
                    otherAccount.SubtractAmount(amount);
                    this.AddAmount(amount);
                }
            }

            Console.WriteLine("[{0}] Transfering {1:C0} from account {2} to {3}",
                Thread.CurrentThread.Name, amount,
                otherAccount.AccountNumber, this.AccountNumber);
        }

        static void ChooseLocks(BankAccountWithChooseLocks accountOne, BankAccountWithChooseLocks accountTwo,
            out object firstLock, out object secondLock)
        {
            if (accountOne.AccountNumber < accountTwo.AccountNumber)
            {
                firstLock = accountOne.lockObject;
                secondLock = accountTwo.lockObject;
            }
            else
            {
                firstLock = accountTwo.lockObject;
                secondLock = accountOne.lockObject;
            }
        }
    }
}