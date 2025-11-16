using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using System.Collections.Generic;

namespace Invoices.Api.Managers
{
    /// <summary>
    /// Manager vrstva obsahuje aplikační logiku spojenou s entitou Invoice.
    /// Odděluje controller od přístupu k datům a umožňuje snadnější testování a údržbu.
    /// </summary>
    public class InvoiceManager : IInvoiceManager
    {
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Vytváří instanci manageru pro práci s fakturami.
        /// </summary>
        /// <param name="invoiceRepository">Repozitář pro přístup k datům faktur.</param>
        /// <param name="mapper">Automapper pro převod mezi entitami a DTO.</param>
        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vrátí seznam všech faktur.
        /// </summary>
        public IEnumerable<InvoiceGetDto> GetAllInvoices()
        {
            // Repository musí používat Include(i => i.Seller).Include(i => i.Buyer),
            // jinak budou navigační vlastnosti null.
            IEnumerable<Invoice> invoices = invoiceRepository.GetAll();
            return mapper.Map<IEnumerable<InvoiceGetDto>>(invoices);
        }

        /// <summary>
        /// Vrátí detail faktury podle ID.
        /// </summary>
        public InvoiceGetDto? GetInvoiceById(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return null;

            return mapper.Map<InvoiceGetDto>(invoice);
        }

        /// <summary>
        /// Přidá novou fakturu.
        /// </summary>
        public InvoiceGetDto AddInvoice(InvoicePostDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice addedInvoice = invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();

            // Vracíme GetDto, aby odpověď obsahovala celé objekty seller/buyer
            return mapper.Map<InvoiceGetDto>(addedInvoice);
        }

        /// <summary>
        /// Smaže fakturu podle ID.
        /// </summary>
        public bool DeleteInvoice(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return false;

            invoiceRepository.Delete(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Vrátí seznam faktur podle kritéria.
        /// </summary>
        public IEnumerable<InvoiceGetDto> FilterInvoices(string criteria)
        {
            IEnumerable<Invoice> invoices = invoiceRepository.Filter(criteria);
            return mapper.Map<IEnumerable<InvoiceGetDto>>(invoices);
        }

        /// <summary>
        /// Aktualizuje existující fakturu.
        /// </summary>
        public InvoiceGetDto UpdateInvoice(InvoicePostDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice updatedInvoice = invoiceRepository.Update(invoice);
            invoiceRepository.SaveChanges();

            // Vracíme GetDto, aby odpověď obsahovala celé objekty seller/buyer
            return mapper.Map<InvoiceGetDto>(updatedInvoice);
        }
    }
}
