
namespace SistemaContableUI.Model
{
    public class TransactionStore
    {
        private readonly List<TransactionEntry> _entries = [];

        public void AddEntry(TransactionEntry entry)
        {
            _entries.Add(entry);
        }

    }
}
