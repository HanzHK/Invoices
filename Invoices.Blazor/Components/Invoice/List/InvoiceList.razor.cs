using Invoices.Shared.Models.Invoice;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System;

namespace Invoices.Blazor.Components.Invoice.List;

public partial class InvoiceList
{
    [Parameter] public IEnumerable<InvoiceGetDto>? Items { get; set; }

    private static readonly CultureInfo CzechCulture = new CultureInfo("cs-CZ");
}