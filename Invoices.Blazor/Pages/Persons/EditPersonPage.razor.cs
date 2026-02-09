using Invoices.Blazor.Pages.Infrastructure.Localization;
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

        protected override async Task OnInitializedAsync()
        {
            var dto = await PersonService.GetByIdAsync(Id);

            if (dto is null)
            {
                Snackbar.Add(T("PersonNotFound"), Severity.Warning);
                Nav.NavigateTo("/subjects/list");
                return;
            }

            person = dto;
        }

        private async Task Save(PersonDto updated)
        {
            await PersonService.ReplaceAsync(updated.PersonId, updated);

            Snackbar.Add(T("PersonUpdated"), Severity.Success);

            Nav.NavigateTo("/subjects/list");
        }
    }
}
