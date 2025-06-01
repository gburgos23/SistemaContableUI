using System.Text.Json.Serialization;

namespace SistemaContableUI.Model
{
    //Transacción que se registra en el sistema contable
    public class TransactionEntry
    {
        [JsonPropertyName("numeroTransaccion")]
        public int TransactionNumber { get; set; }

        [JsonPropertyName("descripcion")]
        public string Description { get; set; }

        [JsonPropertyName("monto")]
        public decimal Amount { get; set; }

        [JsonPropertyName("tipoTransaccion")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType { get; set; }

        [JsonPropertyName("fechaTransaccion")]
        public DateTime TransactionDate { get; set; }
        [JsonPropertyName("pagaIva")]
        public bool isTaxed { get; set; }
    }
}