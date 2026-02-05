using Invoices.Api.Models;

namespace Invoices.Api.Interfaces
{
    /// <summary>
    /// Defines operations for retrieving invoice and person revenue statistics.
    /// </summary>
    /// <remarks>
    /// This interface represents the application-level entry point for statistical data.
    /// Implementations are responsible for orchestrating repository access and delegating
    /// computational logic to specialized calculator services.
    /// </remarks>
    public interface IStatisticsManager
    {
        /// <summary>
        /// Returns aggregated global statistics for all invoices in the system.
        /// </summary>
        /// <returns>
        /// An <see cref="InvoiceStatisticsDto"/> containing:
        /// <list type="bullet">
        /// <item><description>Total revenue for the current year.</description></item>
        /// <item><description>Total revenue across all years.</description></item>
        /// <item><description>Total number of invoices.</description></item>
        /// </list>
        /// </returns>
        InvoiceStatisticsDto GetInvoiceStatistics();

        /// <summary>
        /// Returns revenue statistics grouped by individual persons or companies.
        /// </summary>
        /// <remarks>
        /// Hidden persons are excluded from the result. Only invoices referencing
        /// existing, non-hidden persons are included in the calculation.
        /// </remarks>
        /// <returns>
        /// A collection of <see cref="PersonStatisticsDto"/> objects representing
        /// total revenue per person, ordered by revenue in descending order.
        /// </returns>
        IEnumerable<PersonStatisticsDto> GetPersonStatistics();
    }
}
