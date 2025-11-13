using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public required int InvoiceNumber { get; set; }

        // Kupující a prodávající
        public required Person Seller {  get; set; }
        public required Person Buyer { get; set; }

        // Doplňující údaje
        public required DateTime Issued { get; set; }
        public required DateTime DueDate { get; set; }
        public required string Product { get; set; }
        public required decimal Price { get; set; }
        public required int Vat { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
