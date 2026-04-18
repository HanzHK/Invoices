using System;
using Invoices.Blazor.Services;
using Invoices.Blazor.Services.Localization;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Internal helper that encapsulates all logic related to:
    /// - Subscribing to language-change events
    /// - Unsubscribing on disposal
    /// - Resolving localized strings
    ///
    /// This class is used by ILanguageReactive implementations and should
    /// not be accessed directly outside of extension methods.
    /// </summary>
    public sealed class LanguageReactiveHelper : IDisposable
    {
        private readonly LanguageService _languageService;
        private readonly Action _rerenderCallback;

        /// <summary>
        /// Creates a new helper instance and subscribes to language-change events.
        /// </summary>
        public LanguageReactiveHelper(LanguageService languageService, Action rerenderCallback)
        {
            _languageService = languageService;
            _rerenderCallback = rerenderCallback;

            _languageService.OnChange += _rerenderCallback;
        }

        /// <summary>
        /// Unsubscribes from language-change events to prevent memory leaks.
        /// </summary>
        public void Dispose()
        {
            _languageService.OnChange -= _rerenderCallback;
        }

        /// <summary>
        /// Resolves a localized string using the provided resolver and base name.
        /// </summary>
        public string Resolve(ILocalizationResolver resolver, string key, string baseName)
        {
            if (string.IsNullOrWhiteSpace(key))
                return key ?? string.Empty;

            return resolver.Resolve(key, baseName) ?? key;
        }

        /// <summary>
        /// Resolves a localized string for the specified key using the provided
        /// localization resolver and the component's type to determine the RESX
        /// resource base name.
        /// </summary>
        /// <param name="resolver">
        /// The localization resolver responsible for retrieving localized values
        /// from resource files.
        /// </param>
        /// <param name="componentType">
        /// The component type whose namespace and class name are used to construct
        /// the RESX resource base name. The base name follows the convention:
        /// <c>{Namespace}.{TypeName}</c>.
        /// </param>
        /// <param name="key">
        /// The resource key to resolve. If the key is null, empty, or whitespace,
        /// the original value is returned unchanged.
        /// </param>
        /// <returns>
        /// The localized string if found; otherwise, the original key.
        /// </returns>
        public string T(ILocalizationResolver resolver, Type componentType, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return key ?? string.Empty;

            var ns = componentType.Namespace ?? string.Empty;
            var baseName = string.IsNullOrEmpty(ns)
                ? componentType.Name
                : $"{ns}.{componentType.Name}";

            return resolver.Resolve(key, baseName) ?? key;
        }

    }
}
