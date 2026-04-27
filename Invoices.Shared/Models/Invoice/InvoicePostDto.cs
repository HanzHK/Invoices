using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Invoices.Shared.Models.Invoice
{
    /// <summary>
    /// Simple DTO containing only an identifier.
    /// Used for referencing related entities (e.g., Buyer, Seller).
    /// </summary>
    public class IdOnlyDto
    {
        /// <summary>
        /// Primary key of the referenced entity.
        /// </summary>
        [JsonPropertyName("_id")]
        public int Id { get; set; }
    }

    /// <summary>
    /// Data transfer object used for creating or updating an invoice.
    /// Contains all input fields required from the client.
    /// </summary>
    public class InvoicePostDto
    {
        /// <summary>
        /// Invoice identifier.
        /// Used primarily when updating an existing invoice.
        /// </summary>
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }

        /// <summary>
        /// Sequential invoice number.
        /// </summary>
        [Required(ErrorMessage = "Invoice number is required.")]
        [StringLength(50, ErrorMessage = "Invoice number cannot exceed 50 characters.")]
        public string InvoiceNumber { get; set; } = String.Empty;

        /// <summary>
        /// Seller (supplier) reference, represented only by its ID.
        /// </summary>
        [Required(ErrorMessage = "Seller is required.")]
        public IdOnlyDto Seller { get; set; } = new();

        /// <summary>
        /// Buyer (customer) reference, represented only by its ID.
        /// </summary>
        [Required(ErrorMessage = "Buyer is required.")]
        public IdOnlyDto Buyer { get; set; } = new();

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
        /// Price of the invoice without VAT.
        /// </summary>
        [Range(0, 999999999, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        /// <summary>
        /// VAT rate (percentage).
        /// </summary>
        [Range(0, 100, ErrorMessage = "VAT must be between 0 and 100.")]
        public int Vat { get; set; }

        /// <summary>
        /// Optional note attached to the invoice.
        /// </summary>
        [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters.")]
        public string Note { get; set; } = string.Empty;
    }
}
