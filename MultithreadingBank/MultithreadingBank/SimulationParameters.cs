namespace MultithreadingBank
{
    public class SimulationParameters
    {
        public const double InitialDeposit = 1000;
        public const double MinTransferAmount = 25;
        public const double MaxTransferAmount = 250;
        public const int NumberOfAccounts = 10;
        public const int NumberOfTransferThreads = 4;
        public const int TransferThreadPeriod = 200;
        public const int NumberOfIterations = 20;
        public const int SomeNumberForRandom = 15;

        public const double Precision = 0.003;
    }
}