namespace SistemaContableUI.Service.Reports
{
    public interface IReportService
    {
        void ExportToFile(string type, string reportTitle);
        string GenerateReport(string type, string title);
    }
}