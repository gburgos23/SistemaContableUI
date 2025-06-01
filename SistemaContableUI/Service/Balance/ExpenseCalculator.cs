using SistemaContableUI.Model;

namespace SistemaContableUI.Service.Balance
{
    public class ExpenseCalculator(ITransactionStore transactionStore) : IExpenseCalculator
    {
        private readonly ITransactionStore _transactionStore = transactionStore;
        public decimal Calculate()
        {
            var transactions = _transactionStore.GetAllEntries().ToArray();

            return transactions
                .Where(t => t.TransactionType == TransactionType.Egreso)
                .Sum(t => t.Amount);
        }
    }
}
