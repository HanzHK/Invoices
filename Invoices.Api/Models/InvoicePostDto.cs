using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{

    public class IdOnlyDto
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }
    }
    /// <summary>
    /// DTO pro přenos dat o faktuře přes API.
    /// </summary>
    public class InvoicePostDto
    {
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }

        public int InvoiceNumber { get; set; }

        public IdOnlyDto Seller { get; set; } = new IdOnlyDto();
        public IdOnlyDto Buyer { get; set; } = new IdOnlyDto();

        public DateOnly Issued { get; set; }
        public DateOnly DueDate { get; set; }
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Vat { get; set; }
        public string Note { get; set; } = string.Empty;
    }

}
