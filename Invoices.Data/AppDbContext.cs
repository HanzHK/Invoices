using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabulka osob
        public DbSet<Person> Persons { get; set; }

        // Nová tabulka faktur
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfigurace entity Person
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(p => p.Country)
                       .HasConversion<string>();

                builder.HasIndex(p => p.IdentificationNumber);
                builder.HasIndex(p => p.Hidden);
            });

            // Konfigurace entity Invoice
            modelBuilder.Entity<Invoice>(builder =>
            {
                // Pokud máš enumy (např. stav faktury), můžeš je převést na string
                // builder.Property(i => i.Status).HasConversion<string>();

                // Vztahy: faktura má prodávajícího a kupujícího
                builder.HasOne(i => i.Seller)
                       .WithMany()
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(i => i.Buyer)
                       .WithMany()
                       .OnDelete(DeleteBehavior.Restrict);

                // Index na číslo faktury
                builder.HasIndex(i => i.InvoiceNumber);
                
                builder.Property(i => i.Price)
                        .HasPrecision(18, 2); // 18 číslic celkem, 2 desetinná místa
            });
        }
    }
}
