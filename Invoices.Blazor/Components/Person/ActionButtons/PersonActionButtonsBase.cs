using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services.UI.Actions;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Components.Person.ActionButtons
{
    public partial class PersonActionButtonsBase : LocalizationComponentBase
    {
        [Parameter] public PersonDto PersonModel { get; set; } = default!;

        [Parameter] public EventCallback<PersonDto> OnEdit { get; set; }
        [Parameter] public EventCallback<PersonDto> OnDelete { get; set; }
        [Parameter] public EventCallback<PersonDto> OnDetail { get; set; }

        /// <summary>
        /// Determines whether the Detail action button is rendered.
        /// </summary>
        [Parameter] public bool ShowDetail { get; set; } = true;

        [Inject] protected IPersonActions PersonActions { get; set; } = default!;
        [Inject] protected ISnackbar Snackbar { get; set; } = default!;
    }
}
