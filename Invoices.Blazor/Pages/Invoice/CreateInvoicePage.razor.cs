using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using MudBlazor;


namespace Invoices.Blazor.Pages.Invoice
{
   
    public partial class CreateInvoicePage : LocalizationPageBase
    {
        private InvoicePostDto invoice = new()
        {
            Issued = DateOnly.FromDateTime(DateTime.Today),
            DueDate = DateOnly.FromDateTime(DateTime.Today)
        };
        [Inject] public InvoiceService InvoiceService { get; set; } = default!;

        private async Task CreateInvoiceAsync(InvoicePostDto model)
        {
            var result = await InvoiceService.CreateAsync(model);
            if (!result.Success)
            {
                Snackbar.Add(T("CreateFailed"), Severity.Error);
                return;
            }
            Snackbar.Add(T("InvoiceAdded"), Severity.Success);
            Nav.NavigateTo("/invoices/list");
        }

    }
}
