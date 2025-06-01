
using SistemaContableUI.Model;
using System.Text;

namespace SistemaContableUI.Service.Reports
{
    public class ReportCsv : IReportGenerator
    {
        private readonly TransactionStore _store;

        public ReportCsv(TransactionStore store)
        {
            _store = store;
        }

        public string GenerateReport(string title)
        {
            var data = _store.GetAllEntries().ToArray();
            var builder = new StringBuilder();
            builder.AppendLine("Date,Description,Amount,Type");

            foreach (var entry in data)
            {
                builder.AppendLine($"{DateTime.Now:yyyy-MM-dd},{entry.Description},{entry.Amount},{entry.TransactionType}");
            }

            return builder.ToString();
        }
    }

}
