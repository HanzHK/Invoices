using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Components.Person.Form;
using Invoices.Blazor.Validation;
using Invoices.Blazor.Validation.Specific;
using Invoices.Shared.Models.Invoice;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
namespace Invoices.Blazor.Components.Invoice.Form
{
    public partial class InvoiceForm
    {
    
        [Parameter] public InvoicePostDto Invoice { get; set; } = new();
        [Parameter] public EventCallback<InvoicePostDto> OnSubmit { get; set; }
        [Inject] public FormFieldBlurTracker BlurTracker { get; set; } = default!;
        [Inject] public IStringLocalizerFactory Factory { get; set; } = default!;

        private MudForm? form;
        private FormValidator Validator = default!;


        protected override void OnInitialized()
        {
            base.OnInitialized();
            Validator = new FormValidator(Factory, BlurTracker, typeof(InvoiceForm));
        }
        private async Task Submit()
        {
            await OnSubmit.InvokeAsync(Invoice!);
        }
    }
}
