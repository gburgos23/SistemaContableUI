using SistemaContableUI.Model;
using System.Configuration;

namespace SistemaContableUI.Service.Reports
{
    public class ReportService : IReportService
    {
        private readonly TransactionStore _store;
        private readonly Dictionary<string, IReportGenerator> _generators;
        private readonly string _basePath;

        public ReportService(TransactionStore store)
        {
            _store = store;
            _basePath = ConfigurationManager.AppSettings["ReportBasePath"] ?? "C://Reports";

            _generators = new Dictionary<string, IReportGenerator>
            {
                [ReportType.JSON] = new ReportJson(_store),
                [ReportType.HTML] = new ReportHtml(_store),
                [ReportType.CSV] = new ReportCsv(_store),
                [ReportType.EXCEL] = new ReportXlsx(_store)
            };
        }

        public string GenerateReport(string type, string title)
        {
            if (!_generators.ContainsKey(type))
                throw new NotSupportedException($"Report type '{type}' is not supported.");

            return _generators[type].GenerateReport(title);
        }

        public void ExportToFile(string type, string reportTitle)
        {
            var basePath = ConfigurationManager.AppSettings["ReportBasePath"] ?? "C://Reports";
            string content = GenerateReport(type, reportTitle);

            var path = Path.Combine(basePath, reportTitle, ReportType.JSON);

            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            File.WriteAllText(path, content);
        }
    }
}
