using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Rozhraní pro získávání statistik faktur a osob.
    /// Definuje metody pro obecné statistiky i statistiky jednotlivých společností.
    /// </summary>
    public interface IStatisticsManager
    {
        /// <summary>
        /// Vrátí souhrnné statistiky pro všechny faktury v databázi.
        /// </summary>
        InvoiceStatisticsDto GetInvoiceStatistics();

        /// <summary>
        /// Vrátí statistiky fakturovaných příjmů pro jednotlivé osoby/společnosti.
        /// </summary>
        IEnumerable<PersonStatisticsDto> GetPersonStatistics();
    }
}
