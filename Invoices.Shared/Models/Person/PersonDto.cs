using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json.Serialization;
using Invoices.Shared.Models;
using Invoices.Shared.Models.Common;
namespace Invoices.Shared.Models.Person
{
    public class PersonDto
    {
        [JsonPropertyName("_id")]
        public int PersonId { get; set; }
        /// <summary>
        /// Company name or individual's full name.
        /// </summary>
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name must not exceed 200 characters.")]
        public string Name { get; set; } = "";
        /// <summary>
        /// Business identification number (IČO).
        /// </summary>
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Identification number must be exactly 8 characters.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Identification number must be an 8-digit number.")]
        public string IdentificationNumber { get; set; } = "";
        /// <summary>
        /// Tax identification number (DIČ).
        /// </summary>
        [StringLength(12, ErrorMessage = "Tax number must not exceed 12 characters.")]
        public string TaxNumber { get; set; } = "";
        /// <summary>
        /// Bank account number.
        /// </summary>
        [RegularExpression(@"^\d{1,20}$", ErrorMessage = "Account number must be numeric.")]
        public string AccountNumber { get; set; } = "";
        /// <summary>
        /// Bank code.
        /// </summary>
        [StringLength(4, ErrorMessage = "Bank code must be 4 characters.")]
        public string BankCode { get; set; } = "";
        /// <summary>
        /// IBAN - International Bank Account Number.
        /// </summary>
        [RegularExpression(@"^[A-Z0-9]{15,34}$", ErrorMessage = "IBAN must be valid.")]
        public string Iban { get; set; } = "";
        /// <summary>
        /// Phone number.
        /// </summary>
        [Phone(ErrorMessage = "Phone number is not valid.")]
        public string Telephone { get; set; } = "";
        /// <summary>
        /// Email address.
        /// </summary>
        [EmailAddress(ErrorMessage = "Email address is not valid.")]
        public string Mail { get; set; } = "";
        /// <summary>
        /// Street and house number.
        /// </summary>
        [StringLength(200)]
        public string Street { get; set; } = "";
        /// <summary>
        /// ZIP code.
        /// </summary>
        [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "ZIP code must be in format 12345 or 123 45.")]
        public string Zip { get; set; } = "";
        /// <summary>
        /// City or municipality.
        /// </summary>
        [StringLength(100)]
        public string City { get; set; } = "";
        /// <summary>
        /// Country - see Country enum.
        /// </summary>
        [Required(ErrorMessage = "Country is required.")]
        public Country? Country { get; set; }
        /// <summary>
        /// Optional note.
        /// </summary>
        [StringLength(500)]
        public string Note { get; set; } = "";
    }
}