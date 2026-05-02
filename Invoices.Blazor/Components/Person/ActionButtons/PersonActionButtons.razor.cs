using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Components.Person.ActionButtons
{
    /// <summary>
    /// Action buttons component for person list rows.
    /// Provides Edit and Delete actions for a given person.
    /// </summary>
    public partial class PersonActionButtons : LocalizationComponentBase
    {
        /// <summary>The person this component acts upon.</summary>
        [Parameter] public PersonDto PersonModel { get; set; } = default!;

        /// <summary>Callback triggered after successful deletion.</summary>
        [Parameter] public EventCallback<PersonDto> OnDelete { get; set; }

        [Inject] private PersonService PersonService { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        /// <summary>
        /// Deletes the person via the API and notifies the parent component.
        /// </summary>
        private async Task Delete()
        {
            var result = await PersonService.DeleteAsync(PersonModel.PersonId);
            if (!result.Success)
            {
                Snackbar.Add(T("DeleteFailed"), Severity.Error);
                return;
            }
            Snackbar.Add(T("PersonDeleted"), Severity.Success);
            await OnDelete.InvokeAsync(PersonModel);
        }
    }
}