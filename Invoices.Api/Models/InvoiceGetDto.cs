using Invoices.Api.Models;
using System.Text.Json.Serialization;

/// <summary>
/// Datový objekt pro vracení detailů faktury směrem ven z API - GET.
/// Obsahuje identifikátor, základní údaje o faktuře a informace o dodavateli a odběrateli.
/// </summary>
public class InvoiceGetDto
{
    /// <summary>
    /// Primární klíč faktury v databázi.
    /// JsonPropertyName slouží k tomu aby se správně při vstupu mohla Json data vkládat s parametrem "_id" dle zadání v dokumentaci.  
    /// </summary>
    [JsonPropertyName("_id")]
    public int InvoiceId { get; set; }

    /// <summary>
    /// Číslo faktury (interní nebo účetní).
    /// </summary>
    public int InvoiceNumber { get; set; }

    /// <summary>
    /// Dodavatel (Seller) spojený s fakturou.
    /// </summary>
    public PersonDto Seller { get; set; } = new PersonDto();

    /// <summary>
    /// Odběratel (Buyer) spojený s fakturou.
    /// </summary>
    public PersonDto Buyer { get; set; } = new PersonDto();

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
