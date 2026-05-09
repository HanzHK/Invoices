using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Components.Invoice.Form;
using Invoices.Blazor.Services;
using Invoices.Blazor.Services.CountryAlias;
using Invoices.Blazor.Validation;
using Invoices.Blazor.Validation.Specific;
using Invoices.Shared.Models.Common;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Invoices.Blazor.Components.Person.Form
{
    public partial class PersonForm : LocalizationComponentBase
    {
        [Inject] public ICountryAliasService CountryAliasService { get; set; } = default!;
        [Inject] public IStringLocalizerFactory Factory { get; set; } = default!;
        [Inject] public FormFieldBlurTracker BlurTracker { get; set; } = default!;

        private MudForm? form;
        private FormValidator Validator = default!;
        private AccountNumberModulo11Validator AccountValidator = default!;

        [Parameter] public PersonDto Person { get; set; } = new();
        [Parameter] public bool IsEdit { get; set; }
        [Parameter] public EventCallback<PersonDto> OnSubmit { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Validator = new FormValidator(Factory, BlurTracker, typeof(PersonForm));
            AccountValidator = new AccountNumberModulo11Validator(
                Factory.Create(typeof(PersonForm)),
                Factory.Create(typeof(FormValidator)),
                BlurTracker
            );
    
        }

        private async Task SubmitInternal()
        {
            if (form is null) return;
            await form.Validate();
            if (!form.IsValid)
                return;
            await OnSubmit.InvokeAsync(Person);
        }

        private string GetCountryAlias(Country country)
        {
            return CountryAliasService.GetAlias(country);
        }

        private MudSelect<Country?>? countrySelect;

        private async Task ValidateCountry()
        {
            if (countrySelect is not null)
                await countrySelect.Validate();
        }
    }
}