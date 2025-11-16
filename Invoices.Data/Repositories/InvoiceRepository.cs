using Invoices.Data.Entities;
using Invoices.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Data.Repositories
{
    /// <summary>
    /// Repozitář pro práci s entitou Invoice.
    /// Zajišťuje přístup k databázi přes EF Core.
    /// </summary>
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        private readonly AppDbContext context;

        /// <summary>
        /// Vytváří instanci repozitáře pro práci s fakturami.
        /// </summary>
        /// <param name="context">Databázový kontext aplikace.</param>
        public InvoiceRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Vrátí všechny faktury včetně informací o seller a buyer.
        /// </summary>
        public IEnumerable<Invoice> GetAll()
        {
            return context.Invoices
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .ToList();
        }

        /// <summary>
        /// Najde fakturu podle ID včetně informací o seller a buyer.
        /// </summary>
        public Invoice? FindById(int id)
        {
            return context.Invoices
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .FirstOrDefault(i => i.InvoiceId == id);
        }

        /// <summary>
        /// Vrátí seznam faktur podle kritéria (hledá v čísle faktury, jménu seller nebo buyer).
        /// </summary>
        public IEnumerable<Invoice> Filter(string criteria)
        {
            return context.Invoices
                .Include(i => i.Seller)
                .Include(i => i.Buyer)
                .Where(i => i.InvoiceNumber.ToString() == criteria
                         || i.Seller.Name.Contains(criteria)
                         || i.Buyer.Name.Contains(criteria))
                .ToList();
        }

        /// <summary>
        /// Přidá novou fakturu.
        /// </summary>
        public Invoice Add(Invoice invoice)
        {
            context.Invoices.Add(invoice);
            return invoice;
        }

        /// <summary>
        /// Aktualizuje existující fakturu.
        /// </summary>
        public Invoice Update(Invoice invoice)
        {
            context.Invoices.Update(invoice);
            return invoice;
        }

        /// <summary>
        /// Smaže fakturu.
        /// </summary>
        public void Delete(Invoice invoice)
        {
            context.Invoices.Remove(invoice);
        }

        /// <summary>
        /// Uloží změny do databáze.
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
