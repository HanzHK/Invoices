using Invoices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tabulka osob
        public DbSet<Person> Persons { get; set; }

        // Tabulka faktur
        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Person
            modelBuilder.Entity<Person>(builder =>
            {
                builder.Property(p => p.Country)
                       .HasConversion<string>();

                builder.HasIndex(p => p.IdentificationNumber);
                builder.HasIndex(p => p.Hidden);
            });

            // Invoice
            modelBuilder.Entity<Invoice>(builder =>
            {
                builder.HasOne(i => i.Seller)
                       .WithMany()
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasOne(i => i.Buyer)
                       .WithMany()
                       .OnDelete(DeleteBehavior.Restrict);

                builder.HasIndex(i => i.InvoiceNumber);

                // Price - SQLite nepodporuje HasPrecision
                // builder.Property(i => i.Price).HasPrecision(18, 2);

                // DateOnly konverze - BEZ HasColumnType("date")
                builder.Property(i => i.Issued)
                       .HasConversion(
                           v => v.ToDateTime(TimeOnly.MinValue),
                           v => DateOnly.FromDateTime(v));

                builder.Property(i => i.DueDate)
                       .HasConversion(
                           v => v.ToDateTime(TimeOnly.MinValue),
                           v => DateOnly.FromDateTime(v));
            });
        }

    }
}
