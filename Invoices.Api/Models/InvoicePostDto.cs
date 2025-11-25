using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// Jednoduchý DTO obsahující pouze identifikátor.
    /// Používá se pro referenci na jiné entity (např. Buyer, Seller).
    /// </summary>
    public class IdOnlyDto
    {
        /// <summary>
        /// Primární klíč entity v databázi.
        /// </summary>
        [JsonPropertyName("_id")]
        public int Id { get; set; }
    }

    /// <summary>
    /// Datový objekt pro vytvoření nebo aktualizaci faktury přes API -CREATe, PUT...
    /// Obsahuje vstupní data zasílaná klientem.
    /// </summary>
    public class InvoicePostDto
    {
        /// <summary>
        /// Identifikátor faktury.
        /// Využivá se například při aktualizaci faktury
        /// </summary>
        [JsonPropertyName("_id")]
        public int InvoiceId { get; set; }

        /// <summary>
        /// Číslo faktury.
        /// </summary>
        public int InvoiceNumber { get; set; }

        /// <summary>
        /// Dodavatel (Seller), pouze pomocí jeho ID.
        /// </summary>
        public IdOnlyDto Seller { get; set; } = new IdOnlyDto();

        /// <summary>
        /// Odběratel (Buyer), pouze pomocí jeho ID.
        /// </summary>
        public IdOnlyDto Buyer { get; set; } = new IdOnlyDto();

        /// <summary>
        /// Datum vystavení faktury.
        /// </summary>
        public DateOnly Issued { get; set; }

        /// <summary>
        /// Datum splatnosti faktury.
        /// </summary>
        public DateOnly DueDate { get; set; }

        /// <summary>
        /// Název produktu nebo služby uvedený na faktuře.
        /// </summary>
        public string Product { get; set; } = string.Empty;

        /// <summary>
        /// Cena faktury bez DPH.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Sazba DPH (v procentech).
        /// </summary>
        public int Vat { get; set; }

        /// <summary>
        /// Volitelná poznámka k faktuře.
        /// </summary>
        public string Note { get; set; } = string.Empty;
    }
}
