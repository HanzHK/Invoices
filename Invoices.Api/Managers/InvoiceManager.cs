using AutoMapper;
using Invoices.Api.Interfaces;
using Invoices.Api.Models;
using Invoices.Data.Entities;
using Invoices.Data.Interfaces;

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

        public InvoiceManager(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            this.invoiceRepository = invoiceRepository;
            this.mapper = mapper;
        }

        public IEnumerable<InvoiceDto> GetAllInvoices()
        {
            IEnumerable<Invoice> invoices = invoiceRepository.GetAll();
            return mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public InvoiceDto? GetInvoiceById(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return null;

            return mapper.Map<InvoiceDto>(invoice);
        }

        public InvoiceDto AddInvoice(InvoiceDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice addedInvoice = invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();
            return mapper.Map<InvoiceDto>(addedInvoice);
        }

        public bool DeleteInvoice(int id)
        {
            Invoice? invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return false;

            invoiceRepository.Delete(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }


        public IEnumerable<InvoiceDto> FilterInvoices(string criteria)
        {
            
            IEnumerable<Invoice> invoices = invoiceRepository.Filter(criteria);
            return mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public InvoiceDto UpdateInvoice(InvoiceDto dto)
        {
            Invoice invoice = mapper.Map<Invoice>(dto);
            Invoice updatedInvoice = invoiceRepository.Update(invoice);
            invoiceRepository.SaveChanges();
            return mapper.Map<InvoiceDto>(updatedInvoice);
        }
    }
}
