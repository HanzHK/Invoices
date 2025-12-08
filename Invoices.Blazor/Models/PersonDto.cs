using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Invoices.Blazor.Models
{
    public class PersonDto
    {
        [JsonPropertyName("_id")]
        public int PersonId { get; set; }

        /// <summary>
        /// Název společnosti nebo jméno fyzické osoby.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Identifikační číslo (IČO).
        /// </summary>
        public string IdentificationNumber { get; set; } = "";

        /// <summary>
        /// Daňové identifikační číslo (DIČ).
        /// </summary>
        public string TaxNumber { get; set; } = "";

        /// <summary>
        /// Číslo bankovního účtu.
        /// </summary>
        public string AccountNumber { get; set; } = "";

        /// <summary>
        /// Kód banky.
        /// </summary>
        public string BankCode { get; set; } = "";

        /// <summary>
        /// IBAN - mezinárodní číslo účtu.
        /// </summary>
        public string Iban { get; set; } = "";

        /// <summary>
        /// Telefonní číslo.
        /// </summary>
        public string Telephone { get; set; } = "";

        /// <summary>
        /// E-mailová adresa.
        /// </summary>
        public string Mail { get; set; } = "";

        /// <summary>
        /// Ulice a číslo popisné.
        /// </summary>
        public string Street { get; set; } = "";

        /// <summary>
        /// PSČ - Poštovní Směrovací Číslo.
        /// </summary>
        public string Zip { get; set; } = "";

        /// <summary>
        /// Město (nebo Obec).
        /// </summary>
        public string City { get; set; } = "";

        /// <summary>
        /// Stát - v enum Country
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Volitelná poznámka.
        /// </summary>
        public string Note { get; set; } = "";
    }
}
