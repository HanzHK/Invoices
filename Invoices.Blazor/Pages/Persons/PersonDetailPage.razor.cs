using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Persons
{
    /// <summary>
    /// Page for displaying details of a single person.
    /// Loads the person on initialization and redirects back to the list
    /// if the person cannot be retrieved.
    /// </summary>
    public partial class PersonDetailPage
    {
        private PersonDto? person;

        [Inject] public PersonService PersonService { get; set; } = default!;
        [Parameter] public int Id { get; set; }

        /// <summary>
        /// Loads the person details.
        /// If the person cannot be retrieved, the user is redirected back to the list.
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            var result = await PersonService.GetByIdAsync(Id);

            if (!result.Success)
            {
                Snackbar.Add(T("PersonNotFound"), Severity.Error);
                Nav.NavigateTo("/subjects/list");
                return;
            }

            person = result.Value!;
        }

        /// <summary>
        /// Navigates back to the list after a delete action.
        /// Actual deletion is handled elsewhere.
        /// </summary>
        private Task DeletePerson(PersonDto person)
        {
            Nav.NavigateTo("/subjects/list");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Navigates to the edit page for the selected person.
        /// </summary>
        private void EditPerson(PersonDto person)
        {
            Nav.NavigateTo($"/subjects/edit/{person.PersonId}");
        }
    }
}
