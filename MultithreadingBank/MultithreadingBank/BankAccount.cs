namespace MultithreadingBank
{
    public abstract class BankAccount
    {
        public abstract void AddAmount(double amount);
        public abstract void SubtractAmount(double amount);

        public abstract void TransferFrom(BankAccount otherAccount, double amount);

        public int AccountNumber;
        public double Balance;
        protected internal double balance;
    }
}