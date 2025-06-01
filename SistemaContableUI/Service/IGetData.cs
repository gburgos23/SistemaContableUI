namespace SistemaContableUI.Service
{
    public interface IGetData
    {
        void GetTransaction();
        void PrintAllTransactions();
        void PrintTransactionByDescription();
        void PrintTransactionById();
    }
}