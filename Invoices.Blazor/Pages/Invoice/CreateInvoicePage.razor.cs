using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace Invoices.Blazor.Pages.Invoice
{
   
    public partial class CreateInvoicePage : LocalizationPageBase
    {
        private InvoicePostDto invoice = new();
        [Inject] public InvoiceService InvoiceService { get; set; } = default!;

        private async Task CreateInvoiceAsync(InvoicePostDto model)
        {
            var created = await InvoiceService.CreateAsync(model);
            if (created != null)
            {
                Snackbar.Add(T("InvoiceAdded"), Severity.Success);
                Nav.NavigateTo("/invoices/list");
                return;
            }
        }
    }
}
