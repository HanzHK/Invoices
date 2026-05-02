using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Blazor.Validation;
using Invoices.Blazor.Validation.Specific;
using Invoices.Shared.Models.Invoice;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Globalization;

namespace Invoices.Blazor.Components.Invoice.Form
{
    public partial class InvoiceForm
    {
        [Parameter] public InvoicePostDto Invoice { get; set; } = new();
        [Parameter] public EventCallback<InvoicePostDto> OnSubmit { get; set; }
        [Parameter] public bool IsEdit { get; set; }

        [Inject] public FormFieldBlurTracker BlurTracker { get; set; } = default!;
        [Inject] public IStringLocalizerFactory Factory { get; set; } = default!;
        [Inject] public PersonService PersonService { get; set; } = default!;

        private MudForm? form;
        private FormValidator Validator = default!;
        private static readonly CultureInfo CzechCulture = new CultureInfo("cs-CZ");
        private List<PersonDto> _persons = new();

        private PersonDto? _sellerBacking;
        private PersonDto? _seller
        {
            get => _sellerBacking;
            set
            {
                _sellerBacking = value;
                if (value != null) Invoice.Seller.Id = value.PersonId;
            }
        }

        private PersonDto? _buyerBacking;
        private PersonDto? _buyer
        {
            get => _buyerBacking;
            set
            {
                _buyerBacking = value;
                if (value != null) Invoice.Buyer.Id = value.PersonId;
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Validator = new FormValidator(Factory, BlurTracker, typeof(InvoiceForm));
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await PersonService.GetAllAsync();
            if (result.Success)
                _persons = result.Value ?? new();

            if (IsEdit)
            {
                var sellerResult = await PersonService.GetByIdAsync(Invoice.Seller.Id);
                var buyerResult = await PersonService.GetByIdAsync(Invoice.Buyer.Id);

                if (sellerResult.Success) _sellerBacking = sellerResult.Value;
                if (buyerResult.Success) _buyerBacking = buyerResult.Value;
            }
        }

        private async Task Submit()
        {
            await OnSubmit.InvokeAsync(Invoice!);
        }

        private Task<IEnumerable<PersonDto>> SearchPersons(string value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Task.FromResult(_persons.AsEnumerable());
            return Task.FromResult(_persons.Where(p =>
                p.Name.Contains(value, StringComparison.OrdinalIgnoreCase) ||
                p.IdentificationNumber.Contains(value, StringComparison.OrdinalIgnoreCase)));
        }
    }
}