using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manager vrstva obsahuje aplikační logiku spojenou se statistikami faktur a osob.
    /// </summary>
    public class StatisticsManager : IStatisticsManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IPersonRepository personRepository;

        public StatisticsManager(IInvoiceRepository invoiceRepository, IPersonRepository personRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.personRepository = personRepository;
        }

        /// <summary>
        /// Vrátí souhrnné statistiky pro všechny faktury v databázi.
        /// </summary>
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

        /// <summary>
        /// Vrátí statistiky fakturovaných příjmů pro jednotlivé společnosti.
        /// </summary>
        public IEnumerable<PersonStatisticsDto> GetPersonStatistics()
        {
            var persons = personRepository.GetAll();

            var stats = persons.Select(p => new PersonStatisticsDto
            {
                PersonId = p.PersonId,
                PersonName = p.Name,
                Revenue = invoiceRepository.GetAll()
                    .Where(i => i.SellerId == p.PersonId)
                    .Sum(i => i.Price)
            });

            return stats.ToList();
        }
    }
}
