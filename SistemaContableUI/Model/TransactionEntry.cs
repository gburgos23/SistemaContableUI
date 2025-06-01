
namespace SistemaContableUI.Model
{
    public class TransactionEntry
    {
        public int TransactionNumber { get; set; }
        public decimal Description { get; set; }
        public float Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
