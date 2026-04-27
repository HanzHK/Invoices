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

        /// <summary>
        /// Removes a person from the local list after a delete action.
        /// Actual deletion is handled elsewhere.
        /// </summary>
        private Task DeletePerson(PersonDto person)
        {
            persons = persons!
                .Where(p => p.PersonId != person.PersonId)
                .ToList();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Navigates to the edit page for the selected person.
        /// </summary>
        private void EditPerson(PersonDto person)
        {
            Nav.NavigateTo($"/subjects/edit/{person.PersonId}");
        }

        /// <summary>
        /// Navigates to the detail page for the selected person.
        /// </summary>
        private void ViewPersonDetails(PersonDto person)
        {
            Nav.NavigateTo($"subjects/detail/{person.PersonId}");
        }
    }
}
