using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
namespace Invoices.Blazor.Pages.Persons
{
    public partial class EditPersonPage
    {
        [Inject] public PersonService PersonService { get; set; } = default!;
        [Inject] public NavigationManager Nav { get; set; } = default!;

        [Parameter] public int Id { get; set; }

        private PersonDto? person = null;

        protected override async Task OnInitializedAsync()
        {
            var dto = await PersonService.GetByIdAsync(Id);

            if (dto is null)
            {
                Nav.NavigateTo("/persons");
                return;
            }

            person = dto;
        }

        private async Task Save(PersonDto updated)
        {
            await PersonService.ReplaceAsync(updated.PersonId, updated);
            Nav.NavigateTo("/persons");
        }
    }
}
