using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// DTO pro přenos dat o faktuře přes API.
    /// </summary>
    /// 
    public class InvoiceDto
    {
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }

        public int InvoiceNumber { get; set; }

        public PersonDto Seller { get; set; } = new PersonDto();
        public PersonDto Buyer { get; set; } = new PersonDto();

        public DateTime Issued { get; set; }
        public DateTime DueDate { get; set; }

        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Vat { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
