using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Invoices.Blazor.Components.Invoice.Detail
{
    /// <summary>
    /// Component displaying full details of a single invoice.
    /// Shows header info, seller/buyer summary cards, and financial details.
    /// </summary>
    public partial class InvoiceDetail : LocalizationComponentBase
    {
        /// <summary>The invoice to display.</summary>
        [Parameter] public InvoiceGetDto Invoice { get; set; } = default!;

        /// <summary>Callback triggered after successful deletion.</summary>
        [Parameter] public EventCallback<InvoiceGetDto> OnDelete { get; set; }

        [Inject] private NavigationManager Nav { get; set; } = default!;

        private static readonly CultureInfo CzechCulture = new CultureInfo("cs-CZ");
    }
}