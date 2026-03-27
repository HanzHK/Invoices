using Invoices.Blazor.Services.Localization;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Services.UI.Actions
{
    public class PersonActions : IPersonActions
    {
        private readonly NavigationManager _nav;
        private readonly PersonService _service;

        public PersonActions(NavigationManager nav, PersonService service)
        {
            _nav = nav;
            _service = service;
        }

        public Task Edit(PersonDto person)
        {
            _nav.NavigateTo($"/subjects/edit/{person.PersonId}");
            return Task.CompletedTask;
        }

        public async Task Delete(PersonDto person)
        {
            await _service.DeleteAsync(person.PersonId);
        }

        public Task View(PersonDto person)
        {
            _nav.NavigateTo($"/subjects/detail/{person.PersonId}");
            return Task.CompletedTask;
        }
    }


}
