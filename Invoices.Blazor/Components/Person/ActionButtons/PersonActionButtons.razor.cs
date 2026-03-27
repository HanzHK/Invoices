using Invoices.Blazor.Services.UI.Actions;
using MudBlazor;

namespace Invoices.Blazor.Components.Person.ActionButtons
{
    public partial class PersonActionButtons
    {
        private async Task Delete()
        {
            await PersonActions.Delete(PersonModel);

            Snackbar.Add(T("PersonDeleted"), Severity.Success);

            await OnDelete.InvokeAsync(PersonModel);
        }
    }
}
