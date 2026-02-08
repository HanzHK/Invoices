using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Pages.Persons
{
    public partial class ListOfPersons
    {
        [Inject] public PersonService PersonService { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        private List<PersonDto> persons = new();

        protected override async Task OnInitializedAsync()
        {
            persons = await PersonService.GetAllAsync();
        }
        private async Task DeletePerson(PersonDto person)
        {
            await PersonService.DeleteAsync(person.PersonId);
            persons.Remove(person);
        }
        private void EditPerson(PersonDto person)
        {
            Nav.NavigateTo($"/persons/edit/{person.PersonId}");
        }
        private void ViewPersonDetails(PersonDto person)
            {
            // Navigate to the details page or open a modal for viewing details
        }
    }
}
