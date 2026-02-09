using Invoices.Blazor.Pages.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Persons
{
    /// <summary>
    /// Page displaying a list of subjects (persons).
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

        protected override async Task OnInitializedAsync()
        {
            try
            {
                persons = await PersonService.GetAllAsync();
            }
            catch
            {
                Snackbar.Add(T("LoadFailed"), Severity.Error);
                persons = new();
            }
        }

        private async Task DeletePerson(PersonDto person)
        {
            await PersonService.DeleteAsync(person.PersonId);

            persons = persons!
                .Where(p => p.PersonId != person.PersonId)
                .ToList();

            Snackbar.Add(T("PersonDeleted"), Severity.Success);
        }

        private void EditPerson(PersonDto person)
        {
            Nav.NavigateTo($"/subjects/edit/{person.PersonId}");
        }

        private void ViewPersonDetails(PersonDto person)
        {
            // Navigate to details page or open modal
        }
    }
}
