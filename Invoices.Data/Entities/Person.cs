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

namespace Invoices.Data.Entities
{
    /// <summary>
    /// Entita představující osobu nebo firmu ve fakturačním systému
    /// Obsahuje všechny kontaktní a identifikační údaje
    /// </summary>
    public class Person
    {
        public int PersonId { get; set; }

        // Vlastnosti označené jako 'required' musí být nastaveny při vytváření instance (od .NET 7)
        public required string Name { get; set; }
        public required string IdentificationNumber { get; set; }
        public required string TaxNumber { get; set; }
        public required string AccountNumber { get; set; }
        public required string BankCode { get; set; }
        public required string Iban { get; set; }
        public required string Telephone { get; set; }
        public required string Mail { get; set; }
        public required string Street { get; set; }
        public required string Zip { get; set; }
        public required string City { get; set; }

        // Enum Country je v databázi uložen jako string (viz konfigurace v AppDbContext)
        public required Country Country { get; set; }


        // Označení záznamu jako "skrytého" místo fyzického smazání (tzv. soft delete)
        public bool Hidden { get; set; }

        public required string Note { get; set; }

        // Faktury prodejů a nákupů
        public List<Invoice>? Purchases;
        public List<Invoice>? Sales;

    }
}