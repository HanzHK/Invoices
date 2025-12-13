using Invoices.Data;
using Invoices.Data.Entities;
using Invoices.Data.Entities.Enums;

namespace Invoices.Api.Seeding
{
    public class SampleDataSeeder
    {
        private readonly AppDbContext _db;

        public SampleDataSeeder(AppDbContext db)
        {
            _db = db;
        }

        public void SeedPersons()
        {
            if (_db.Persons.Any()) return;

            _db.Persons.AddRange(
                new Person
                {
                    Name = "Jan Novák",
                    IdentificationNumber = "12345678",
                    TaxNumber = "CZ12345678",
                    AccountNumber = "1234567890",
                    BankCode = "0100",
                    Iban = "CZ6501000000001234567890",
                    Telephone = "+420123456789",
                    Mail = "jan.novak@example.com",
                    Street = "Ulice 1",
                    Zip = "10000",
                    City = "Praha",
                    Country = Country.CZECHIA,
                    Note = "Testovací osoba",
                    Hidden = false
                },
                new Person
                {
                    Name = "Anna Horváthová",
                    IdentificationNumber = "87654321",
                    TaxNumber = "SK87654321",
                    AccountNumber = "0987654321",
                    BankCode = "0900",
                    Iban = "SK8909000000000987654321",
                    Telephone = "+421987654321",
                    Mail = "anna.horvathova@example.sk",
                    Street = "Cesta 5",
                    Zip = "81101",
                    City = "Bratislava",
                    Country = Country.SLOVAKIA,
                    Note = "Druhá osoba",
                    Hidden = false
                },
                new Person
                {
                    Name = "Petr Svoboda",
                    IdentificationNumber = "11223344",
                    TaxNumber = "CZ11223344",
                    AccountNumber = "111122223333",
                    BankCode = "0300",
                    Iban = "CZ65030000000011223344",
                    Telephone = "+420777111222",
                    Mail = "petr.svoboda@example.cz",
                    Street = "Masarykova 10",
                    Zip = "60200",
                    City = "Brno",
                    Country = Country.CZECHIA,
                    Note = "Třetí testovací osoba",
                    Hidden = false
                },
                new Person
                {
                    Name = "Lucie Dvořáková",
                    IdentificationNumber = "55667788",
                    TaxNumber = "CZ55667788",
                    AccountNumber = "444455556666",
                    BankCode = "0800",
                    Iban = "CZ65080000000055667788",
                    Telephone = "+420608555666",
                    Mail = "lucie.dvorakova@example.cz",
                    Street = "Plzeňská 25",
                    Zip = "30100",
                    City = "Plzeň",
                    Country = Country.CZECHIA,
                    Note = "Čtvrtá testovací osoba",
                    Hidden = false
                },
                new Person
                {
                    Name = "Ján Kováč",
                    IdentificationNumber = "99887766",
                    TaxNumber = "SK99887766",
                    AccountNumber = "777788889999",
                    BankCode = "1100",
                    Iban = "SK89110000000099887766",
                    Telephone = "+421905888999",
                    Mail = "jan.kovac@example.sk",
                    Street = "Hlavná 15",
                    Zip = "04001",
                    City = "Košice",
                    Country = Country.SLOVAKIA,
                    Note = "Pátá testovací osoba",
                    Hidden = false
                },
                new Person
                {
                    Name = "Zuzana Nováková",
                    IdentificationNumber = "44556677",
                    TaxNumber = "SK44556677",
                    AccountNumber = "222233334444",
                    BankCode = "1200",
                    Iban = "SK89120000000044556677",
                    Telephone = "+421944333444",
                    Mail = "zuzana.novakova@example.sk",
                    Street = "Námestie SNP 3",
                    Zip = "81101",
                    City = "Bratislava",
                    Country = Country.SLOVAKIA,
                    Note = "Šestá testovací osoba",
                    Hidden = false
                }
            );

            _db.SaveChanges();
        }
    }
}
