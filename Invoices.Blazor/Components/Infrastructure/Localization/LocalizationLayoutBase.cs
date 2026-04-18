using Microsoft.AspNetCore.Components;
using Invoices.Blazor.Services;
using Invoices.Blazor.Services.Localization;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Base class for all localized Blazor layouts.
    /// Implements ILanguageReactive to enable:
    /// - Automatic UI rerender on language change
    /// - Shared localization logic via extension methods
    /// - Centralized subscription management through LanguageReactiveHelper
    ///
    /// Layouts inheriting from this class can directly call T("Key")
    /// without any additional boilerplate.
    /// </summary>
    public abstract class LocalizationLayoutBase
        : LayoutComponentBase, ILanguageReactive
    {
        /// <summary>
        /// Localization resolver injected by Blazor DI.
        /// Used by extension methods to resolve resource keys.
        /// </summary>
        [Inject]
        public ILocalizationResolver Resolver { get; set; } = default!;

        /// <summary>
        /// Language service injected by Blazor DI.
        /// Raises events when the current language changes.
        /// </summary>
        [Inject]
        public LanguageService LanguageService { get; set; } = default!;

        /// <summary>
        /// Internal helper instance that manages subscriptions and
        /// localization resolution. Assigned during initialization.
        /// </summary>
        public LanguageReactiveHelper? ReactiveHelper { get; set; }

        /// <summary>
        /// Connects Blazor layout lifecycle with language-change reactivity.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.InitializeLanguageReactivity();
        }

        /// <summary>
        /// Triggers a UI rerender when the language changes.
        /// </summary>
        public void TriggerRerender() => StateHasChanged();

        /// <summary>
        /// Ensures proper cleanup of language-change subscriptions.
        /// </summary>
        public void Dispose() => this.DisposeLanguageReactivity();

        /// <summary>
        /// Resolves a localized string for the specified key using the component's
        /// own type to determine the RESX resource base name. This method delegates
        /// the actual resolution to the underlying LanguageReactiveHelper.
        /// </summary>
        /// <param name="key">
        /// The resource key to resolve. If the key is null, empty, or whitespace,
        /// the original value is returned unchanged.
        /// </param>
        /// <returns>
        /// The localized string if found; otherwise, the original key.
        /// </returns>
        protected string T(string key) =>
            ReactiveHelper!.T(Resolver, GetType(), key);

    }
}
