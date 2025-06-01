

using SistemaContableUI.Model;
using System.Globalization;
using System.Text;

namespace SistemaContableUI.Service.Reports
{
    public class ReportHtml(ITransactionStore store) : IReportGenerator
    {
        private readonly ITransactionStore _store = store;

        public string GenerateReport(string title)
        {
            var data = _store.GetAllEntries().ToArray();

            var builder = new StringBuilder();
            builder.AppendLine(title);
            builder.AppendLine("------------------------------");

            foreach (var entry in data)
            {
                string sign = entry.TransactionType == TransactionType.Ingreso ? "$" : "-$";
                builder.AppendLine($"{entry.TransactionNumber} - {entry.TransactionDate} -> {entry.Description} - {sign}{entry.Amount:N0} ({entry.TransactionType})");
            }

            return builder.ToString();
        }
    }

}
