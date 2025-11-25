/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *
 *                      ___ ___ ___
 *                     | . |  _| . |  LICENCE
 *                     |  _|_| |___|
 *                     |_|
 *
 *    REKVALIFIKAČNÍ KURZY  <>  PROGRAMOVÁNÍ  <>  IT KARIÉRA
 *
 * Tento zdrojový kód je součástí profesionálních IT kurzů na
 * WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci PRO obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */
using Invoices.Data.Entities.Enums;
using System.Text.Json.Serialization;

namespace Invoices.Api.Models
{
    /// <summary>
    /// Datový objekt pro přenos informací o osobě nebo společnosti přes API.
    /// Odděluje vnější datovou strukturu (JSON pro frontend) od vnitřní databázové entity.
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Primární klíč společnosti v databázi.
        /// V JSONu chceme použít "_id"´místo PersonId.
        /// </summary>
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
