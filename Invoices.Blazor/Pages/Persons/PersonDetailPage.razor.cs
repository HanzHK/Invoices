using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Persons
{
    public partial class PersonDetailPage
    {
        private PersonDto? person;

        [Inject] public PersonService PersonService { get; set; } = default!;
        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                person = await PersonService.GetByIdAsync(Id);

                if (person is null)
                {
                    Snackbar.Add(T("PersonNotFound"), Severity.Error);
                    Nav.NavigateTo("/subjects/list");
                    return;
                }
            }
            catch
            {
                Snackbar.Add(T("LoadFailed"), Severity.Error);
                Nav.NavigateTo("/subjects/list");
            }
        }

    }
}
