using SistemaContableUI.Model;
using SistemaContableUI.Service.Balance;
using SistemaContableUI.Service.Reports;
using System.Text.Json;

namespace SistemaContableUI.Service
{
    public class PrincipalMenu(IIncomeCalculator incomeCalculator,
        IExpenseCalculator expenseCalculator,
        IBalanceCalculator balanceCalculator,
        IReportService reportService,
        IGetData getData)
    {
        private readonly IIncomeCalculator _incomeCalculator = incomeCalculator;
        private readonly IExpenseCalculator _expenseCalculator = expenseCalculator;
        private readonly IBalanceCalculator _balanceCalculator = balanceCalculator;
        private readonly IReportService _reportService = reportService;
        private readonly IGetData _getData = getData;


        public void ShowMainMenu()
        {
            bool exit = false;

            try
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("=== MENÚ PRINCIPAL ===");
                    Console.WriteLine("1. Agregar transacción");
                    Console.WriteLine("2. Ver todas las transacciones");
                    Console.WriteLine("3. Generar balance");
                    Console.WriteLine("4. Buscar transacción por ID");
                    Console.WriteLine("5. Buscar por descripción");
                    Console.WriteLine("6. Generar reporte");
                    Console.WriteLine("0. Salir");
                    Console.Write("Seleccione una opción: ");

                    string input = Console.ReadLine();
                    Console.Clear();

                    switch (input)
                    {
                        case "1":
                            _getData.GetTransaction();
                            break;

                        case "2":

                            _getData.PrintAllTransactions();
                            break;

                        case "3":
                            bool backFromBalance = false;
                            do
                            {
                                Console.WriteLine("=== SUBMENÚ BALANCE ===");
                                Console.WriteLine("1. Ver total de ingresos");
                                Console.WriteLine("2. Ver total de egresos");
                                Console.WriteLine("3. Ver balance");
                                Console.WriteLine("0. Volver al menú principal");
                                Console.Write("Seleccione una opción: ");
                                string balanceOption = Console.ReadLine();
                                Console.Clear();

                                switch (balanceOption)
                                {
                                    case "1":
                                        Console.WriteLine($"Total de ingresos: ${_incomeCalculator.Calculate()}");
                                        break;
                                    case "2":
                                        Console.WriteLine($"Total de egresos: ${_expenseCalculator.Calculate()}");
                                        break;
                                    case "3":
                                        _balanceCalculator.GetBalance();
                                        break;
                                    case "0":
                                        backFromBalance = true;
                                        break;
                                    default:
                                        Console.WriteLine("Opción inválida.");
                                        break;
                                }

                                if (!backFromBalance)
                                {
                                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                            } while (!backFromBalance);
                            break;

                        case "4":
                            _getData.PrintTransactionById();
                            break;

                        case "5":
                            _getData.PrintTransactionByDescription();
                            break;

                        case "6":
                            bool backFromReports = false;
                            do
                            {
                                Console.WriteLine("=== SUBMENÚ REPORTES ===");
                                Console.WriteLine("1. Generar reporte en CSV");
                                Console.WriteLine("2. Generar reporte en HTML"); 
                                Console.WriteLine("3. Generar reporte en JSON");
                                Console.WriteLine("4. Generar reporte en Excel");
                                Console.WriteLine("0. Volver al menú principal");
                                Console.Write("Seleccione una opción: ");
                                string reportTitle = $"ReporteTransacciones - {DateTime.Now:yyyy-MM-dd}";
                                string reportOption = Console.ReadLine();
                                string exportOption = string.Empty;
                                Console.Clear();

                                switch (reportOption)
                                {
                                    case "1":
                                        reportTitle = $"ReporteTransacciones - {DateTime.Now:yyyy-MM-dd}";
                                        _reportService.GenerateReport(ReportType.CSV, reportTitle);

                                        do
                                        {
                                            Console.Write("¿Desea exportar el reporte a un archivo? (S/N): ");
                                            exportOption = (Console.ReadLine() ?? "").Trim().ToUpper();
                                            if (exportOption == "S")
                                            {
                                                _reportService.ExportToFile(ReportType.CSV, reportTitle);
                                                Console.WriteLine("Reporte exportado correctamente.");
                                            }
                                            else if (exportOption == "N")
                                            {
                                                // No exporta, solo regresa al submenú
                                            }
                                            else
                                            {
                                                Console.WriteLine("Debe ingresar 'S' para Sí o 'N' para No.");
                                            }
                                        } while (exportOption != "S" && exportOption != "N");

                                        break;
                                    case "2":
                                        reportTitle = $"ReporteTransacciones - {DateTime.Now:yyyy-MM-dd}";
                                        _reportService.GenerateReport(ReportType.HTML, reportTitle);

                                        do
                                        {
                                            Console.Write("¿Desea exportar el reporte a un archivo? (S/N): ");
                                            exportOption = (Console.ReadLine() ?? "").Trim().ToUpper();
                                            if (exportOption == "S")
                                            {
                                                _reportService.ExportToFile(ReportType.HTML, reportTitle);
                                                Console.WriteLine("Reporte exportado correctamente.");
                                            }
                                            else if (exportOption == "N")
                                            {
                                                // No exporta, solo regresa al submenú
                                            }
                                            else
                                            {
                                                Console.WriteLine("Debe ingresar 'S' para Sí o 'N' para No.");
                                            }
                                        } while (exportOption != "S" && exportOption != "N");
                                        break;
                                    case "3":
                                        reportTitle = $"ReporteTransacciones - {DateTime.Now:yyyy-MM-dd}";
                                        _reportService.GenerateReport(ReportType.JSON, reportTitle);

                                        do
                                        {
                                            Console.Write("¿Desea exportar el reporte a un archivo? (S/N): ");
                                            exportOption = (Console.ReadLine() ?? "").Trim().ToUpper();
                                            if (exportOption == "S")
                                            {
                                                _reportService.ExportToFile(ReportType.JSON, reportTitle);
                                                Console.WriteLine("Reporte exportado correctamente.");
                                            }
                                            else if (exportOption == "N")
                                            {
                                                // No exporta, solo regresa al submenú
                                            }
                                            else
                                            {
                                                Console.WriteLine("Debe ingresar 'S' para Sí o 'N' para No.");
                                            }
                                        } while (exportOption != "S" && exportOption != "N");
                                        break;
                                    case "4":
                                        reportTitle = $"ReporteTransacciones - {DateTime.Now:yyyy-MM-dd}";
                                        _reportService.GenerateReport(ReportType.EXCEL, reportTitle);
                                        break;
                                    case "0":
                                        backFromReports = true;
                                        break;
                                    default:
                                        Console.WriteLine("Opción inválida.");
                                        break;
                                }

                                if (!backFromReports)
                                {
                                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                                    Console.ReadKey();
                                    Console.Clear();
                                }

                            } while (!backFromReports);
                            break;

                        case "0":
                            exit = true;
                            Console.WriteLine("Saliendo del programa...");
                            break;

                        default:
                            Console.WriteLine("Opción inválida. Intente de nuevo.");
                            break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                    }

                } while (!exit);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
