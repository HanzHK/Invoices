using Invoices.Api.Models;
using System.Text.Json.Serialization;

public class InvoiceGetDto
{
    [JsonPropertyName("_id")]
    public int InvoiceId { get; set; }
    public int InvoiceNumber { get; set; }

    public PersonDto Seller { get; set; } = new PersonDto();
    public PersonDto Buyer { get; set; } = new PersonDto();

    public DateOnly Issued { get; set; }
    public DateOnly DueDate { get; set; }
    public string Product { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Vat { get; set; }
    public string Note { get; set; } = string.Empty;
}
