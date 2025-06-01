
namespace SistemaContableUI.Model
{
    public class TransactionStore : ITransactionStore
    {
        private readonly List<TransactionEntry> _entries = [];
        private int _nextTransactionNumber = 1;

        public void AddEntry(TransactionEntry entry)
        {
            _entries.Add(entry);
            _nextTransactionNumber++;
        }

        public bool RemoveEntry(int transactionNumber)
        {
            var entry = _entries.FirstOrDefault(e => e.TransactionNumber == transactionNumber);
            if (entry == null)
                return false;

            _entries.Remove(entry);
            return true;
        }

        public IEnumerable<TransactionEntry> GetAllEntries()
        {
            return _entries;
        }

        public TransactionEntry? GetEntryByNumber(int transactionNumber)
        {
            return _entries.FirstOrDefault(e => e.TransactionNumber == transactionNumber);
        }

        public void ClearAll()
        {
            _entries.Clear();
            _nextTransactionNumber = 1;
        }

    }
}
