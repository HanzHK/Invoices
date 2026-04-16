using Invoices.Blazor.Services;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Base class that reacts to language changes by refreshing the UI.
    /// Shared by both pages and components to avoid duplication.
    /// </summary>
    public abstract class LanguageReactiveBase : ComponentBase, IDisposable
    {
        [Inject] protected LanguageService Language { get; set; } = default!;

        protected override void OnInitialized()
        {
            Language.OnChange += HandleLanguageChanged;
            base.OnInitialized();
        }

        private void HandleLanguageChanged()
        {
            OnLanguageChanged();
            InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Optional override for pages/components that need to reload data.
        /// </summary>
        protected virtual void OnLanguageChanged() { }

        public void Dispose()
        {
            Language.OnChange -= HandleLanguageChanged;
        }
    }
}
