
using SistemaContableUI.Model;
using System.Text;

namespace SistemaContableUI.Service.Reports
{
    public class ReportCsv(TransactionStore store) : IReportGenerator
    {
        private readonly TransactionStore _store = store;

        public string GenerateReport(string title)
        {
            var data = _store.GetAllEntries().ToArray();
            var builder = new StringBuilder();
            builder.AppendLine("numeroTransaccion,descripcion,monto,tipoTransaccion,fechaTransaccion,pagaIva");

            foreach (var entry in data)
            {
                builder.AppendLine($"{entry.TransactionNumber},{entry.Description},{entry.Amount},{entry.TransactionType},{entry.TransactionDate},{(entry.isTaxed ? "SI" : "NO")}");
            }

            return builder.ToString();
        }
    }
}
