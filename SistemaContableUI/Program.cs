using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.Extensions.DependencyInjection;
using SistemaContableUI.Model;
using SistemaContableUI.Service;
using SistemaContableUI.Service.Balance;
using SistemaContableUI.Service.Reports;

var services = new ServiceCollection();

services.AddSingleton<ITransactionStore, TransactionStore>();
services.AddScoped<IBalanceCalculator, BalanceCalculator>();
services.AddScoped<IExpenseCalculator, ExpenseCalculator>();
services.AddScoped<IIncomeCalculator, IncomeCalculator>();
services.AddScoped<IReportService, ReportService>();
services.AddScoped<IGetData, GetData>(); 

var serviceProvider = services.BuildServiceProvider();

// Resolver las dependencias requeridas para PrincipalMenu
var incomeCalculator = serviceProvider.GetRequiredService<IIncomeCalculator>();
var expenseCalculator = serviceProvider.GetRequiredService<IExpenseCalculator>();
var balanceCalculator = serviceProvider.GetRequiredService<IBalanceCalculator>();
var reportService = serviceProvider.GetRequiredService<IReportService>();
var getData = serviceProvider.GetRequiredService<IGetData>();

// Crear instancia de PrincipalMenu con las dependencias
PrincipalMenu menu = new(incomeCalculator, expenseCalculator, balanceCalculator, reportService, getData);
menu.ShowMainMenu();
