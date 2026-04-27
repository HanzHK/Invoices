using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Persons
{
    /// <summary>
    /// Page for editing an existing person.
    /// Uses LocalizationPageBase to provide:
    /// - Localized strings via T()
    /// - Snackbar notifications
    /// - Navigation helpers
    /// </summary>
    public partial class EditPersonPage : LocalizationPageBase
    {
        [Inject] public PersonService PersonService { get; set; } = default!;

        [Parameter] public int Id { get; set; }

        private PersonDto? person;

        /// <summary>
        /// Loads the person to be edited.
        /// If the person cannot be retrieved, the user is redirected back to the list.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var result = await PersonService.GetByIdAsync(Id);

            if (!result.Success)
            {
                Snackbar.Add(T("PersonNotFound"), Severity.Warning);
                Nav.NavigateTo("/subjects/list");
                return;
            }

            person = result.Value!;
        }

        /// <summary>
        /// Saves the updated person.
        /// Displays an error message if the update fails.
        /// </summary>
        private async Task Save(PersonDto updated)
        {
            var result = await PersonService.ReplaceAsync(updated.PersonId, updated);

            if (!result.Success)
            {
                Snackbar.Add(result.Error ?? T("PersonUpdateFailed"), Severity.Error);
                return;
            }

            Snackbar.Add(T("PersonUpdated"), Severity.Success);
            Nav.NavigateTo("/subjects/list");
        }
    }
}
