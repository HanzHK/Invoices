using Invoices.Blazor.InputHandlers;
using Invoices.Blazor.Services;
using Invoices.Blazor.Validation;
using Invoices.Blazor.Validation.Rules;
using Invoices.Blazor.Validation.Specific;
using Invoices.Shared.Models.Common;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;

namespace Invoices.Blazor.Components.Person.Form
{
    public partial class PersonForm
    {
        [Inject] public IStringLocalizer<PersonForm> L { get; set; } = default!;
        [Inject] public LanguageService Lang { get; set; } = default!;
        [Inject] public IStringLocalizerFactory Factory { get; set; } = default!;
        [Inject] public FormFieldBlurTracker BlurTracker { get; set; } = default!;

        private MudForm? form;
        private RulesValidator Rules;
        private FormValidator Validator;
        private AccountNumberModulo11Validator AccountValidator;

        [Parameter] public PersonDto Person { get; set; } = new();
        [Parameter] public bool IsEdit { get; set; }
        [Parameter] public EventCallback<PersonDto> OnSubmit { get; set; }

        protected override void OnInitialized()
        {
            Validator = new FormValidator(Factory, BlurTracker, typeof(PersonForm));
            AccountValidator = new AccountNumberModulo11Validator(
                                        Factory.Create(typeof(PersonForm)),     // primary
                                        Factory.Create(typeof(FormValidator)),  // fallback
                                        BlurTracker
            );


            Lang.OnChange += Refresh;
        }

        private async Task SubmitInternal()
        {
            await form.Validate();

            if (!form.IsValid)
                return;

            await OnSubmit.InvokeAsync(Person);
        }

        private string GetCountryAlias(Country country)
        {
            return L[country.ToString()];
        }

        private void Refresh()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Lang.OnChange -= Refresh;
        }
        private MudSelect<Country?>? countrySelect;


        private async Task ValidateCountry()
        {
            if (countrySelect is not null)
                await countrySelect.Validate();
        }

    }
}
