using SistemaContableUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;

namespace SistemaContableUI.Service
{
    public class GetData(ITransactionStore transactionStore) : IGetData
    {
        private readonly ITransactionStore _transactionStore = transactionStore;
        private static readonly JsonSerializerOptions cachedJsonSerializerOptions = new() { WriteIndented = true };
        public void GetTransaction()
        {

            TransactionEntry transactionEntry = new();

            // Solicitar descripción
            string descripcion;
            do
            {
                Console.Write("Descripción: ");
                descripcion = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(descripcion))
                    Console.WriteLine("La descripción no puede estar vacía.");
            } while (string.IsNullOrWhiteSpace(descripcion));

            // Solicitar tipo (Ingreso/Egreso)
            string tipo;
            do
            {
                Console.Write("Tipo (I = Ingreso, E = Egreso): ");
                tipo = (Console.ReadLine() ?? "").Trim().ToUpper();
                if (tipo != "I" && tipo != "E")
                    Console.WriteLine("Debe ingresar 'I' para Ingreso o 'E' para Egreso.");
            } while (tipo != "I" && tipo != "E");

            // Solicitar monto
            decimal monto;
            while (true)
            {
                Console.Write("Monto: ");
                string montoInput = Console.ReadLine() ?? "";
                if (decimal.TryParse(montoInput, out monto))
                {
                    if (tipo == "E" && monto >= 0)
                    {
                        Console.WriteLine("Para egresos, el monto debe ser menor a 0.");
                        continue;
                    }
                    if (tipo == "I" && monto < 0)
                    {
                        Console.WriteLine("Para ingresos, el monto no puede ser menor a 0.");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("El monto debe ser un número válido.");
            }

            // Solicitar fecha
            DateTime fecha;
            while (true)
            {
                Console.Write("Fecha (yyyy-MM-dd): ");
                string fechaInput = Console.ReadLine() ?? "";
                if (DateTime.TryParse(fechaInput, out fecha))
                    break;
                Console.WriteLine("La fecha debe tener el formato correcto (yyyy-MM-dd).");
            }

            // Solicitar si aplica IVA
            bool aplicaIva;
            while (true)
            {
                Console.Write("¿Aplica IVA? (S/N): ");
                string ivaInput = (Console.ReadLine() ?? "").Trim().ToUpper();
                if (ivaInput == "S")
                {
                    aplicaIva = true;
                    break;
                }
                else if (ivaInput == "N")
                {
                    aplicaIva = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Debe ingresar 'S' para Sí o 'N' para No.");
                }
            }

            transactionEntry.Description = descripcion;
            transactionEntry.Amount = monto;
            transactionEntry.TransactionType = tipo == "I" ? TransactionType.Ingreso : TransactionType.Egreso;
            transactionEntry.TransactionDate = DateTime.Parse(fecha.ToString("yyyy-MM-dd"));
            transactionEntry.isTaxed = aplicaIva;

            _transactionStore.AddEntry(transactionEntry);
            Console.WriteLine("Transacción agregada exitosamente.");
        }

        // Imprime todas las transacciones almacenadas
        public void PrintAllTransactions()
        {
            var transactions = _transactionStore.GetAllEntries();
            Console.WriteLine("------------- Transacciones ----------");
            foreach (var transaccion in transactions)
            {
                string json = JsonSerializer.Serialize(transaccion, cachedJsonSerializerOptions);
                Console.WriteLine(json);
            }
        }

        //Imprime una transacción específica por su ID
        public void PrintTransactionById()
        {
            int searchId;
            while (true)
            {
                Console.Write("Ingrese el número de transacción: ");
                string idInput = Console.ReadLine() ?? "";
                if (int.TryParse(idInput, out searchId))
                    break;
                Console.WriteLine("Debe ingresar un número válido.");
            }
            var transaction = _transactionStore.GetEntryByNumber(searchId);
            if (transaction != null)
            {

                string json = JsonSerializer.Serialize(transaction, cachedJsonSerializerOptions);
                Console.WriteLine(json);
            }

            else
            {
                Console.WriteLine("No se encontró ninguna transacción con ese ID.");
            }
        }

        //Imprime transacciones por descripción
        public void PrintTransactionByDescription()
        {
            string searchDescription;
            do
            {
                Console.Write("Ingrese la descripción a buscar: ");
                searchDescription = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(searchDescription))
                    Console.WriteLine("La descripción no puede estar vacía.");
            } while (string.IsNullOrWhiteSpace(searchDescription));

            var transactions = _transactionStore.GetEntryByDescription(searchDescription);
            if (transactions?.Count > 0)
            {
                foreach (var transaccion in transactions)
                {
                    string json = JsonSerializer.Serialize(transaccion, cachedJsonSerializerOptions);
                    Console.WriteLine(json);
                }
            }
            else
            {
                Console.WriteLine("No se encontron transacciones con esa descripción.");
            }
        }
    }
}
