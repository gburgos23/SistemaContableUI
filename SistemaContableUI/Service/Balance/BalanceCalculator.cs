using SistemaContableUI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContableUI.Service.Balance
{
    // Calcula el balance total de ingresos y egresos, incluyendo IVA
    public class BalanceCalculator(
        IIncomeCalculator incomeCalculator,
        IExpenseCalculator expenseCalculator,
        ITransactionStore transactionStore) : IBalanceCalculator
    {
        private readonly IIncomeCalculator _incomeCalculator = incomeCalculator;
        private readonly IExpenseCalculator _expenseCalculator = expenseCalculator;
        private readonly ITransactionStore _transactionStore = transactionStore;

        public void GetBalance()
        {
            var income = _incomeCalculator.Calculate();
            var expense = _expenseCalculator.Calculate();
            var balance = income - expense;
            var tax = int.Parse(ConfigurationManager.AppSettings["tax"] ?? "15");

            var totalTax = CalculateTotalTax(tax);

            Console.WriteLine(
                $"Total, ingresos: ${income:N0}\n" +
                $"Total, egresos: ${expense:N0}\n" +
                $"Balance: ${balance:N0}\n" +
                $"IVA ({tax}%): ${totalTax:N0}");
        }
        private decimal CalculateTotalTax(int tax)
        {
            var transactions = _transactionStore.GetAllEntries();
            return transactions
                .Where(t => t.isTaxed)
                .Sum(t => Math.Abs(t.Amount) * (tax / 100));
        }
    }
}
