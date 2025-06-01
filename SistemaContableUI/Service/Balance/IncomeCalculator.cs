using SistemaContableUI.Model;


namespace SistemaContableUI.Service.Balance
{
    public class IncomeCalculator(ITransactionStore transactionStore) : IIncomeCalculator
    {
        private readonly ITransactionStore _transactionStore = transactionStore;
        public decimal Calculate()
        {
            var transactions = _transactionStore.GetAllEntries().ToArray();

            return transactions
                .Where(t => t.TransactionType == TransactionType.Ingreso)
                .Sum(t => t.Amount);
        }
    }

}
