using Invoices.Blazor.Services.Localization;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Provides extension methods for ILanguageReactive implementations.
    /// These methods encapsulate:
    /// - Initialization of language-change reactivity
    /// - Cleanup of subscriptions
    /// - Localized string resolution (T)
    /// - Resource base name generation
    ///
    /// This keeps all shared behavior in one place and avoids code duplication
    /// across components, layouts, and pages.
    /// </summary>
    public static class LanguageReactiveExtensions
    {
        /// <summary>
        /// Initializes language reactivity by creating a helper instance
        /// that subscribes to LanguageService.OnChange.
        /// </summary>
        public static void InitializeLanguageReactivity(this ILanguageReactive component)
        {
            component.ReactiveHelper = new LanguageReactiveHelper(
                component.LanguageService,
                component.TriggerRerender
            );
        }

        /// <summary>
        /// Disposes the helper and unsubscribes from language-change events.
        /// </summary>
        public static void DisposeLanguageReactivity(this ILanguageReactive component)
        {
            component.ReactiveHelper?.Dispose();
        }

        /// <summary>
        /// Resolves a localized string using the component's resolver and
        /// automatically derived resource base name.
        /// </summary>
        public static string T(this ILanguageReactive component, string key)
        {
            var baseName = component.BuildResourceBaseName();
            return component.ReactiveHelper?.Resolve(component.Resolver, key, baseName) ?? key;
        }

        /// <summary>
        /// Builds the RESX resource base name using the convention:
        /// {Namespace}.{TypeName}.
        /// </summary>
        public static string BuildResourceBaseName(this ILanguageReactive component)
        {
            var type = component.GetType();
            var ns = type.Namespace ?? string.Empty;
            return string.IsNullOrEmpty(ns) ? type.Name : $"{ns}.{type.Name}";
        }
    }
}
