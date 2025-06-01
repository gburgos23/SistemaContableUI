
namespace SistemaContableUI.Model
{
    public interface ITransactionStore
    {
        void AddEntry(TransactionEntry entry);
        void ClearAll();
        IEnumerable<TransactionEntry> GetAllEntries();
        TransactionEntry? GetEntryByNumber(int transactionNumber);
        List<TransactionEntry>? GetEntryByDescription(string description);
        bool RemoveEntry(int transactionNumber);
    }
}