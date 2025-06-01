
using ClosedXML.Excel;
using SistemaContableUI.Model;
using System.Configuration;
using System.IO;

namespace SistemaContableUI.Service.Reports
{
    public class ReportXlsx(ITransactionStore store) : IReportGenerator
    {
        private readonly ITransactionStore _store = store;

        public string GenerateReport(string title)
        {
            var data = _store.GetAllEntries().ToArray();

            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Report");

                worksheet.Cell(1, 1).Value = "Numero Transaccion";
                worksheet.Cell(1, 2).Value = "Descripcion";
                worksheet.Cell(1, 3).Value = "Monto";
                worksheet.Cell(1, 4).Value = "Tipo Transaccion";
                worksheet.Cell(1, 5).Value = "Fecha Transaccion";
                worksheet.Cell(1, 6).Value = "Aplica IVA";

                for (int i = 0; i < data.Length; i++)
                {
                    var entry = data[i];
                    worksheet.Cell(i + 2, 1).Value = entry.TransactionNumber;
                    worksheet.Cell(i + 2, 2).Value = entry.Description;
                    worksheet.Cell(i + 2, 3).Value = entry.Amount;
                    worksheet.Cell(i + 2, 4).Value = entry.TransactionType.ToString();
                    worksheet.Cell(i + 2, 5).Value = entry.TransactionDate;
                    worksheet.Cell(i + 2, 6).Value = entry.isTaxed ? "SI" : "NO";
                    ;
                }

                var basePath = ConfigurationManager.AppSettings["ReportBasePath"] ?? "C://Reports";
                var dir = Path.Combine(basePath, title);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir!);
                }

                var filePath = Path.Combine(dir, $"{title}.xlsx");
                workbook.SaveAs(filePath);

                return $"El reporte se genero en la ruta: {filePath}";
            }
            catch (Exception ex)
            {
                return $"Error al generar el reporte: {ex.Message}";
            }
        }
    }

}
