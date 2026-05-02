using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Persons
{
    /// <summary>
    /// Page displaying a list of persons.
    /// Provides:
    /// - Localized UI strings via T()
    /// - Snackbar notifications
    /// - Navigation helpers
    /// - Loading state
    /// - Error handling
    /// </summary>
    public partial class ListOfPersons : LocalizationPageBase
    {
        [Inject] public PersonService PersonService { get; set; } = default!;

        private List<PersonDto>? persons;

        /// <summary>
        /// Loads all persons from the API.
        /// Displays an error message if loading fails.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var result = await PersonService.GetAllAsync();

            if (!result.Success)
            {
                Snackbar.Add(T("LoadFailed"), Severity.Error);
                persons = new();
                return;
            }

            persons = result.Value!;
        }

    }
}
