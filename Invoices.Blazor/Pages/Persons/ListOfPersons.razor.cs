using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Pages.Persons
{
    public partial class ListOfPersons
    {
        [Inject] public PersonService PersonService { get; set; } = default!;

        private List<PersonDto> persons = new();

        protected override async Task OnInitializedAsync()
        {
            persons = await PersonService.GetAllAsync();
        }
    }
}
