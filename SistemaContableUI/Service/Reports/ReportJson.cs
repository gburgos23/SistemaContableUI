using SistemaContableUI.Model;
using System.Configuration;
using System.Text.Json;

namespace SistemaContableUI.Service.Reports
{
    public class ReportJson(ITransactionStore transactionStore):IReportGenerator
    {
        private static readonly JsonSerializerOptions cachedJsonSerializerOptions = new() { WriteIndented = true };
        private readonly ITransactionStore _transactionStore = transactionStore;

        public string GenerateReport(string reportTitle)
        {
            var entries = _transactionStore.GetAllEntries().ToArray();

            var report = new
            {
                Titulo = reportTitle,
                Fecha = DateTime.Now,
                TotalTransacciones = entries.Length,
                Transacciones = entries
            };

            return JsonSerializer.Serialize(report, cachedJsonSerializerOptions);
        }
    }
}
