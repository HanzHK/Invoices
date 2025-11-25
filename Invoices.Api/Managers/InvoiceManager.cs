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
            var invoices = invoiceRepository.GetAll();
            return mapper.Map<IEnumerable<InvoiceGetDto>>(invoices);
        }

        /// <summary>
        /// Vrátí detail faktury podle ID.
        /// </summary>
        public InvoiceGetDto? GetInvoiceById(int id)
        {
            var invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return null;

            return mapper.Map<InvoiceGetDto>(invoice);
        }

        /// <summary>
        /// Přidá novou fakturu.
        /// </summary>
        public InvoiceGetDto AddInvoice(InvoicePostDto dto)
        {
            var invoice = mapper.Map<Invoice>(dto);
            invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();

            // znovu načteme fakturu i s navigačními vlastnostmi
            var createdInvoice = invoiceRepository.FindById(invoice.InvoiceId);
            return mapper.Map<InvoiceGetDto>(createdInvoice);
        }

        /// <summary>
        /// Aktualizuje existující fakturu.
        /// </summary>
        public InvoiceGetDto? UpdateInvoice(InvoicePostDto dto)
        {
            var invoice = mapper.Map<Invoice>(dto);
            var updatedInvoice = invoiceRepository.Update(invoice);
            invoiceRepository.SaveChanges();

            if (updatedInvoice == null)
                return null;

            // znovu načteme fakturu i s navigačními vlastnostmi
            var refreshedInvoice = invoiceRepository.FindById(updatedInvoice.InvoiceId);
            return mapper.Map<InvoiceGetDto>(refreshedInvoice);
        }

        /// <summary>
        /// Smaže fakturu podle ID.
        /// </summary>
        public bool DeleteInvoice(int id)
        {
            var invoice = invoiceRepository.FindById(id);
            if (invoice is null)
                return false;

            invoiceRepository.Delete(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }

        /// <summary>
        /// Vyhledá a vrátí seznam faktur podle zadaných filtrovacích pravidel.
        /// </summary>
        /// <param name="filter">
        /// Objekt <see cref="InvoiceFilterDto"/> obsahující volitelné parametry filtrování:
        /// <list type="bullet">
        ///   <item>
        ///     <description><c>BuyerId</c> – vybere faktury, kde je odběratelem daná firma.</description>
        ///   </item>
        ///   <item>
        ///     <description><c>SellerId</c> – vybere faktury, kde je dodavatelem daná firma.</description>
        ///   </item>
        ///   <item>
        ///     <description><c>Product</c> – vybere faktury, které obsahují daný produkt.</description>
        ///   </item>
        ///   <item>
        ///     <description><c>MinPrice</c> – vybere faktury s částkou vyšší nebo rovnou této hodnotě.</description>
        ///   </item>
        ///   <item>
        ///     <description><c>MaxPrice</c> – vybere faktury s částkou nižší nebo rovnou této hodnotě.</description>
        ///   </item>
        ///   <item>
        ///     <description><c>Limit</c> – omezí počet vrácených faktur na daný počet.</description>
        ///   </item>
        /// </list>
        /// </param>
        /// <returns>
        /// Kolekce <see cref="InvoiceGetDto"/> odpovídající zadanému filtru.
        /// Pokud není zadán žádný parametr, vrátí všechny faktury.
        /// </returns>
        public IEnumerable<InvoiceGetDto> FilterInvoices(InvoiceFilterDto filter)
        {
            var invoices = invoiceRepository.GetAll();

            if (filter.BuyerId.HasValue)
                invoices = invoices.Where(i => i.BuyerId == filter.BuyerId.Value);

            if (filter.SellerId.HasValue)
                invoices = invoices.Where(i => i.SellerId == filter.SellerId.Value);

            if (!string.IsNullOrEmpty(filter.Product))
                invoices = invoices.Where(i => i.Product.Contains(filter.Product));

            if (filter.MinPrice.HasValue)
                invoices = invoices.Where(i => i.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                invoices = invoices.Where(i => i.Price <= filter.MaxPrice.Value);

            if (filter.Limit.HasValue)
                invoices = invoices.Take(filter.Limit.Value);

            return mapper.Map<IEnumerable<InvoiceGetDto>>(invoices);
        }


        public IEnumerable<InvoiceGetDto> GetIssuedInvoicesByIco(string ico)
        {
            var invoices = invoiceRepository.GetAll()
                .Where(i => i.Seller.IdentificationNumber == ico);

            return mapper.Map<IEnumerable<InvoiceGetDto>>(invoices);
        }

        public IEnumerable<InvoiceGetDto> GetReceivedInvoicesByIco(string ico)
        {
            var invoices = invoiceRepository.GetAll()
                .Where(i => i.Buyer.IdentificationNumber == ico);

            return mapper.Map<IEnumerable<InvoiceGetDto>>(invoices);
        }



    }
}
