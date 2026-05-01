using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Invoice
{
    public partial class EditInvoicePage : LocalizationPageBase
    {
        [Parameter] public int Id { get; set; }
        [Inject] public InvoiceService InvoiceService { get; set; } = default!;

        private InvoicePostDto invoice = new();

        protected override async Task OnInitializedAsync()
        {
            var result = await InvoiceService.GetByIdAsync(Id);
            if (!result.Success)
            {
                Snackbar.Add(T("LoadFailed"), Severity.Error);
                Nav.NavigateTo("/invoices/list");
                return;
            }

            var dto = result.Value!;
            invoice = new InvoicePostDto
            {
                InvoiceId = dto.InvoiceId,
                // TODO: Temporary conversion — InvoiceNumber is string in InvoicePostDto but int in InvoiceGetDto.
                // Unify types once the backend entity and models are refactored.
                InvoiceNumber = dto.InvoiceNumber.ToString(),
                Seller = new IdOnlyDto { Id = dto.Seller.PersonId },
                Buyer = new IdOnlyDto { Id = dto.Buyer.PersonId },
                Issued = dto.Issued,
                DueDate = dto.DueDate,
                Product = dto.Product,
                Price = dto.Price,
                Vat = dto.Vat,
                Note = dto.Note
            };
        }

        private async Task UpdateInvoiceAsync(InvoicePostDto model)
        {
            var result = await InvoiceService.ReplaceAsync(Id, model);
            if (!result.Success)
            {
                Snackbar.Add(T("UpdateFailed"), Severity.Error);
                return;
            }
            Snackbar.Add(T("InvoiceUpdated"), Severity.Success);
            Nav.NavigateTo("/invoices/list");
        }
    }
}