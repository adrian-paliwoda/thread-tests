using System;
using System.Threading;

namespace MultithreadingBank
{
    public class BankAccountWithoutLock : BankAccount
    {
        public new double Balance
        {
            get => balance;
        }


        public BankAccountWithoutLock(int accountNumber, double a_balance)
        {
            AccountNumber = accountNumber;
            balance = a_balance;
        }

        public override void AddAmount(double amt)
        {
            double temp = balance;
            temp += amt;
            Thread.Sleep(1);
            balance = temp;
        }

        public override void SubtractAmount(double amt)
        {
            AddAmount(-amt);
        }

        public override void TransferFrom(BankAccount otherAccount, double amount)
        {
            if (otherAccount is BankAccountWithoutLock accountWithoutLock)
            {
                TransferFrom(accountWithoutLock, amount);
            }
        }

        public void TransferFrom(BankAccountWithoutLock otherAccountWithoutLock, double amt)
        {
            otherAccountWithoutLock.SubtractAmount(amt);
            this.AddAmount(amt);

            Console.WriteLine("[{0}] Transfering {1:C0} from account {2} to {3}",
                Thread.CurrentThread.Name, amt,
                otherAccountWithoutLock.AccountNumber, this.AccountNumber);
        }
    }
}