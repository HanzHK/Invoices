using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Invoice
{
    /// <summary>
    /// Page for displaying details of a single invoice.
    /// Loads the invoice on initialization and redirects back to the list
    /// if the invoice cannot be retrieved.
    /// </summary>
    public partial class InvoiceDetailPage : LocalizationPageBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public InvoiceService InvoiceService { get; set; } = default!;

        private InvoiceGetDto? invoice;

        /// <summary>
        /// Loads the invoice details.
        /// If the invoice cannot be retrieved, the user is redirected back to the list.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var result = await InvoiceService.GetByIdAsync(Id);
            if (!result.Success)
            {
                Snackbar.Add(T("InvoiceNotFound"), Severity.Error);
                Nav.NavigateTo("/invoices/list");
                return;
            }
            invoice = result.Value!;
        }

        /// <summary>
        /// Navigates back to the invoice list after successful deletion.
        /// </summary>
        private void HandleDelete(InvoiceGetDto _)
        {
            Nav.NavigateTo("/invoices/list");
        }
    }
}