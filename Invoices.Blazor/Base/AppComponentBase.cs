using Invoices.Blazor.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Base
{
    public class AppComponentBase : ComponentBase
    {
        [Inject] protected DarkModeSwitchService UiState { get; set; } = default!;
        [Inject] protected IDialogService DialogService { get; set; } = default!;
    }
}
