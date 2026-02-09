using Invoices.Blazor.Services.Localization;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Pages.Infrastructure.Localization
{
    /// <summary>
    /// Base class for Blazor pages that require localized strings
    /// and common UI services (snackbar, navigation).
    /// </summary>
    /// <remarks>
    /// This class mirrors <see cref="LocalizationComponentBase"/> but is intended
    /// specifically for Razor Pages (not components). It provides:
    /// - Automatic RESX base name resolution based on namespace + type name
    /// - T(key) helper for localized strings
    /// - Injected ISnackbar for consistent UI notifications
    /// - Injected NavigationManager for clean redirects
    ///
    /// Convention:
    /// Place a RESX file named <TypeName>.resx next to the page class.
    /// Example:
    ///   Invoices.Blazor.Pages.Persons.EditPersonPage.resx
    /// </remarks>
    public abstract class LocalizationPageBase : ComponentBase
    {
        private ILocalizationResolver? _resolver;

        [Inject]
        protected ILocalizationResolver Resolver
        {
            get => _resolver!;
            set => _resolver = value;
        }

        [Inject]
        protected ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        protected NavigationManager Nav { get; set; } = default!;

        /// <summary>
        /// Resolve a localized string for this page using the derived RESX base name.
        /// </summary>
        protected string T(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return key;

            var baseName = BuildResourceBaseName();
            return Resolver.Resolve(key, baseName);
        }

        /// <summary>
        /// Builds RESX base name as: {Namespace}.{TypeName}
        /// Example:
        ///   Invoices.Blazor.Pages.Persons.EditPersonPage
        /// </summary>
        protected virtual string BuildResourceBaseName()
        {
            var type = GetType();
            var ns = type.Namespace ?? string.Empty;
            var typeName = type.Name;

            return string.IsNullOrEmpty(ns)
                ? typeName
                : $"{ns}.{typeName}";
        }

        /// <summary>
        /// Allows overriding the resolver manually (useful for unit tests).
        /// </summary>
        public void SetResolver(ILocalizationResolver resolver) => Resolver = resolver;
    }
}
