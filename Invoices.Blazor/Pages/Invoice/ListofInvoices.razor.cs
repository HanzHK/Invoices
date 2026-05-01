using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Invoice
{
    /// <summary>
    /// Page displaying a list of all invoices.
    /// Provides:
    /// - Localized UI strings via T()
    /// - Snackbar notifications
    /// - Navigation helpers
    /// - Loading state
    /// - Error handling
    /// </summary>
    public partial class ListofInvoices : LocalizationPageBase
    {
        [Inject] public InvoiceService InvoiceService { get; set; } = default!;

        private List<InvoiceGetDto>? invoices;

        /// <summary>
        /// Loads all invoices from the API on page initialization.
        /// Displays an error snackbar if loading fails.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var result = await InvoiceService.GetAllAsync();
            if (!result.Success)
            {
                Snackbar.Add(T("LoadFailed"), Severity.Error);
                invoices = new();
                return;
            }
            invoices = result.Value!;
        }
    }
}