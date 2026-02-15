using Invoices.Blazor.Pages.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Persons
{
    public partial class CreatePerson : LocalizationPageBase
    {
        private PersonDto person = new();
        [Inject] public PersonService PersonService { get; set; } = default!;
        private async Task CreatePersonAsync(PersonDto model)
        {


            var created = await PersonService.CreateAsync(model);

            if (created != null)
            {
                Snackbar.Add(T("PersonAdded"), Severity.Success);
                Nav.NavigateTo("/subjects/list");
                return;
            }
        }
    }
}
