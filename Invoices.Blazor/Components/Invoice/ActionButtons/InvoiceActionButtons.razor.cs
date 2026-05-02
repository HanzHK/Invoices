using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Components.Invoice.ActionButtons
{
    /// <summary>
    /// Action buttons component for invoice list rows.
    /// Provides Edit and Delete actions for a given invoice.
    /// </summary>
    public partial class InvoiceActionButtons : LocalizationComponentBase
    {
        /// <summary>The invoice this component acts upon.</summary>
        [Parameter] public InvoiceGetDto InvoiceModel { get; set; } = default!;

        /// <summary>Callback triggered after successful deletion.</summary>
        [Parameter] public EventCallback<InvoiceGetDto> OnDelete { get; set; }

        /// <summary>Callback triggered when edit is requested.</summary>
        [Parameter] public EventCallback<InvoiceGetDto> OnEdit { get; set; }

        [Inject] private InvoiceService InvoiceService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;

        /// <summary>
        /// Deletes the invoice via the API and notifies the parent component.
        /// </summary>
        private async Task Delete()
        {
            var result = await InvoiceService.DeleteAsync(InvoiceModel.InvoiceId);
            if (!result.Success)
            {
                Snackbar.Add(T("DeleteFailed"), Severity.Error);
                return;
            }
            Snackbar.Add(T("InvoiceDeleted"), Severity.Success);
            await OnDelete.InvokeAsync(InvoiceModel);
        }
    }
}