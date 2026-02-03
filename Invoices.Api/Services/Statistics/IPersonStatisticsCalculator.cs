using Invoices.Api.Models;
using Invoices.Data.Entities;

namespace Invoices.Api.Services.Statistics
{
    /// <summary>
    /// Defines a contract for calculating revenue statistics for individual persons or companies.
    /// </summary>
    /// <remarks>
    /// The calculator is responsible solely for processing in-memory collections of persons and invoices.
    /// It does not perform any database access. The caller is expected to provide the required data.
    /// 
    /// Typical implementations aggregate invoice values and associate them with the corresponding
    /// person based on their unique identifier.
    /// </remarks>
    public interface IPersonStatisticsCalculator
    {
        /// <summary>
        /// Calculates revenue statistics for the provided collection of persons based on the given invoices.
        /// </summary>
        /// <param name="persons">
        /// A collection of <see cref="Person"/> entities representing individuals or companies.
        /// Hidden or soft-deleted persons should be filtered out by the caller if necessary.
        /// </param>
        /// <param name="invoices">
        /// A collection of <see cref="Invoice"/> entities used to compute revenue totals.
        /// Only invoices associated with persons present in the <paramref name="persons"/> collection
        /// should be considered.
        /// </param>
        /// <returns>
        /// A collection of <see cref="PersonStatisticsDto"/> objects, each containing:
        /// <list type="bullet">
        /// <item><description>The person's identifier.</description></item>
        /// <item><description>The person's display name.</description></item>
        /// <item><description>The total revenue calculated from matching invoices.</description></item>
        /// </list>
        /// </returns>
        IEnumerable<PersonStatisticsDto> Calculate(
            IEnumerable<Person> persons,
            IEnumerable<Invoice> invoices
        );
    }
}
