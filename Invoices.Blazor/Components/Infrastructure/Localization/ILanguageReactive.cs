using System;
using Invoices.Blazor.Services;
using Invoices.Blazor.Services.Localization;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Defines the contract for components and layouts that support
    /// localization and language-change reactivity.
    /// Implementations are expected to:
    /// - Provide access to localization resolver and language service via DI
    /// - Delegate reactive behavior to LanguageReactiveHelper
    /// - Implement TriggerRerender to refresh their UI
    /// </summary>
    public interface ILanguageReactive : IDisposable
    {
        /// <summary>
        /// Localization resolver used to translate resource keys.
        /// Typically injected via Blazor DI.
        /// </summary>
        ILocalizationResolver Resolver { get; }

        /// <summary>
        /// Language service that raises events when the current language changes.
        /// Typically injected via Blazor DI.
        /// </summary>
        LanguageService LanguageService { get; }

        /// <summary>
        /// Triggers a UI rerender. Implementations usually call StateHasChanged()
        /// or an equivalent mechanism.
        /// </summary>
        void TriggerRerender();

        /// <summary>
        /// Holds the internal reactive helper instance that manages subscriptions
        /// and localization logic. Implementations should expose this property
        /// explicitly and not use it directly outside of extensions.
        /// </summary>
        LanguageReactiveHelper? ReactiveHelper { get; set; }
    }
}
