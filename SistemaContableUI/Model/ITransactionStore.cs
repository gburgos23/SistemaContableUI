
namespace SistemaContableUI.Model
{
    public interface ITransactionStore
    {
        void AddEntry(TransactionEntry entry);
        void ClearAll();
        IEnumerable<TransactionEntry> GetAllEntries();
        TransactionEntry? GetEntryByNumber(int transactionNumber);
        bool RemoveEntry(int transactionNumber);
    }
}