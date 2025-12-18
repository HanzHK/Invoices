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
        /// Název společnosti nebo jméno fyzické osoby.
        /// </summary>
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(200, ErrorMessage = "Jméno nesmí být delší než 200 znaků.")]
        public string Name { get; set; } = "";

        /// <summary>
        /// Identifikační číslo (IČO).
        /// </summary>
        [StringLength(8, MinimumLength = 8, ErrorMessage = "IČO musí mít 8 znaků.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "IČO musí být číslo o délce 8.")]
        public string IdentificationNumber { get; set; } = "";

        /// <summary>
        /// Daňové identifikační číslo (DIČ).
        /// </summary>
        [StringLength(12, ErrorMessage = "DIČ nesmí být delší než 12 znaků.")]
        public string TaxNumber { get; set; } = "";

        /// <summary>
        /// Číslo bankovního účtu.
        /// </summary>
        [RegularExpression(@"^\d{1,20}$", ErrorMessage = "Číslo účtu musí být číslo.")]
        public string AccountNumber { get; set; } = "";

        /// <summary>
        /// Kód banky.
        /// </summary>
        [StringLength(4, ErrorMessage = "Kód banky má 4 znaky.")]
        public string BankCode { get; set; } = "";

        /// <summary>
        /// IBAN - mezinárodní číslo účtu.
        /// </summary>
        [RegularExpression(@"^[A-Z0-9]{15,34}$", ErrorMessage = "IBAN musí být platný.")]
        public string Iban { get; set; } = "";

        /// <summary>
        /// Telefonní číslo.
        /// </summary>
        [Phone(ErrorMessage = "Telefonní číslo není platné.")]
        public string Telephone { get; set; } = "";

        /// <summary>
        /// E-mailová adresa.
        /// </summary>
        [EmailAddress(ErrorMessage = "E-mail není platný.")]
        public string Mail { get; set; } = "";

        /// <summary>
        /// Ulice a číslo popisné.
        /// </summary>
        [StringLength(200)]
        public string Street { get; set; } = "";

        /// <summary>
        /// PSČ - Poštovní Směrovací Číslo.
        /// </summary>
        [RegularExpression(@"^\d{3}\s?\d{2}$", ErrorMessage = "PSČ musí být ve formátu 12345 nebo 123 45.")]
        public string Zip { get; set; } = "";

        /// <summary>
        /// Město (nebo Obec).
        /// </summary>
        [StringLength(100)]
        public string City { get; set; } = "";

        /// <summary>
        /// Stát - v enum Country
        /// </summary>
        [Required(ErrorMessage = "Stát je povinný.")]
        public Country? Country { get; set; }

        /// <summary>
        /// Volitelná poznámka.
        /// </summary>
        [StringLength(500)]
        public string Note { get; set; } = "";
    }
}
