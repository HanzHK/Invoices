using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Api.Services.Statistics;
using Invoices.Data.Interfaces;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Provides application-level operations for generating invoice and person revenue statistics.
    /// </summary>
    /// <remarks>
    /// This manager acts as an orchestration layer between repositories and calculation services.
    /// It does not perform any business logic directly; instead, it delegates all statistical
    /// computations to injected calculator services.
    ///
    /// The manager retrieves raw data (persons, invoices) from repositories and passes them
    /// to the appropriate calculator implementation. This design ensures separation of concerns,
    /// improves testability, and keeps the manager lightweight and focused.
    /// </remarks>
    public class StatisticsManager : IStatisticsManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IPersonRepository personRepository;
        private readonly IPersonStatisticsCalculator personStatisticsCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsManager"/> class.
        /// </summary>
        /// <param name="invoiceRepository">Repository providing access to invoice data.</param>
        /// <param name="personRepository">Repository providing access to person data.</param>
        /// <param name="personStatisticsCalculator">
        /// Service responsible for calculating revenue statistics for persons.
        /// </param>
        public StatisticsManager(
            IInvoiceRepository invoiceRepository,
            IPersonRepository personRepository,
            IPersonStatisticsCalculator personStatisticsCalculator
        )
        {
            this.invoiceRepository = invoiceRepository;
            this.personRepository = personRepository;
            this.personStatisticsCalculator = personStatisticsCalculator;
        }

        /// <summary>
        /// Returns aggregated revenue statistics for all visible persons.
        /// </summary>
        /// <remarks>
        /// Hidden persons are automatically excluded by the calculator.
        /// Only invoices referencing an existing, non-hidden person are included.
        /// </remarks>
        /// <returns>
        /// A collection of <see cref="PersonStatisticsDto"/> objects representing
        /// total revenue grouped by person.
        /// </returns>
        public IEnumerable<PersonStatisticsDto> GetPersonStatistics()
        {
            var persons = personRepository.GetAll();
            var invoices = invoiceRepository.GetAll();

            return personStatisticsCalculator.Calculate(persons, invoices);
        }

        /// <summary>
        /// Returns global invoice statistics such as total revenue and invoice count.
        /// </summary>
        /// <remarks>
        /// This method performs simple aggregation directly, as these statistics
        /// do not require complex logic or cross-entity relationships.
        /// </remarks>
        /// <returns>
        /// An <see cref="InvoiceStatisticsDto"/> containing:
        /// <list type="bullet">
        /// <item><description>Total revenue for the current year.</description></item>
        /// <item><description>Total revenue across all years.</description></item>
        /// <item><description>Total number of invoices.</description></item>
        /// </list>
        /// </returns>
        public InvoiceStatisticsDto GetInvoiceStatistics()
        {
            var invoices = invoiceRepository.GetAll();
            var currentYear = DateTime.Now.Year;

            return new InvoiceStatisticsDto
            {
                CurrentYearSum = invoices
                    .Where(i => i.Issued.Year == currentYear)
                    .Sum(i => i.Price),

                AllTimeSum = invoices.Sum(i => i.Price),

                InvoicesCount = invoices.Count()
            };
        }
    }
}
