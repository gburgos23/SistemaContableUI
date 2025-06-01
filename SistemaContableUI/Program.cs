using Microsoft.Extensions.DependencyInjection;
using SistemaContableUI.Model;

var services = new ServiceCollection();

// Registrar el servicio ITransactionStore como singleton
services.AddSingleton<ITransactionStore, TransactionStore>();

services.BuildServiceProvider();



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
