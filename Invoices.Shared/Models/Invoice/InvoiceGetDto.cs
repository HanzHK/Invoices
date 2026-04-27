using Invoices.Shared.Models.Person;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Shared.Models.Invoice
{
    /// <summary>
    /// DTO returned by the API for invoice details (GET).
    /// Contains full invoice information including seller, buyer,
    /// dates, pricing and metadata.
    /// </summary>
    public class InvoiceGetDto
    {
        /// <summary>
        /// Primary key of the invoice in the database.
        /// JsonPropertyName ensures compatibility with the "_id" field
        /// used in the API specification.
        /// </summary>
        [JsonPropertyName("_id")]
        [Required(ErrorMessage = "Invoice ID is required.")]
        public int InvoiceId { get; set; }

        /// <summary>
        /// Invoice number (internal or accounting).
        /// </summary>
        [Required(ErrorMessage = "Invoice number is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Invoice number must be a positive number.")]
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Seller associated with the invoice.
        /// </summary>
        [Required(ErrorMessage = "Seller is required.")]
        public PersonDto Seller { get; set; } = new PersonDto();

        /// <summary>
        /// Buyer associated with the invoice.
        /// </summary>
        [Required(ErrorMessage = "Buyer is required.")]
        public PersonDto Buyer { get; set; } = new PersonDto();

        /// <summary>
        /// Date when the invoice was issued.
        /// </summary>
        [Required(ErrorMessage = "Issued date is required.")]
        public DateOnly Issued { get; set; }

        /// <summary>
        /// Due date of the invoice.
        /// </summary>
        [Required(ErrorMessage = "Due date is required.")]
        public DateOnly DueDate { get; set; }

        /// <summary>
        /// Name of the product or service listed on the invoice.
        /// </summary>
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200, ErrorMessage = "Product name cannot exceed 200 characters.")]
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Price of the invoice excluding VAT.
        /// </summary>
        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public decimal Price { get; set; }

        /// <summary>
        /// VAT rate (percentage).
        /// </summary>
        [Required(ErrorMessage = "VAT rate is required.")]
        [Range(0, 100, ErrorMessage = "VAT must be between 0 and 100.")]
        public int Vat { get; set; }

        /// <summary>
        /// Optional note attached to the invoice.
        /// </summary>
        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string Note { get; set; } = string.Empty;
    }
}
