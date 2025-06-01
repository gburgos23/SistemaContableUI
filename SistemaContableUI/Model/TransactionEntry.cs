using System.Text.Json.Serialization;

namespace SistemaContableUI.Model
{
    public class TransactionEntry
    {
        [JsonPropertyName("numeroTransaccion")]
        public int TransactionNumber { get; set; }

        [JsonPropertyName("descripcion")]
        public string Description { get; set; }

        [JsonPropertyName("monto")]
        public float Amount { get; set; }

        [JsonPropertyName("tipoTransaccion")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType TransactionType { get; set; }

        [JsonPropertyName("fechaTransaccion")]
        public DateTime TransactionDate { get; set; }
    }
}