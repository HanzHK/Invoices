using Invoices.Blazor.Services;
using Invoices.Blazor.Services.Localization;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Base class for components that require localized strings
    /// and automatic UI refresh when the application language changes.
    /// </summary>
    public abstract class LocalizationComponentBase : ComponentBase, IDisposable
    {
        private ILocalizationResolver? _resolver;

        [Inject]
        protected ILocalizationResolver Resolver
        {
            get => _resolver!;
            set => _resolver = value;
        }

        /// <summary>
        /// Provides access to the global LanguageService.
        /// Used to refresh UI when culture changes.
        /// </summary>
        [Inject]
        protected LanguageService Language { get; set; } = default!;

        /// <summary>
        /// Subscribe to language change notifications.
        /// </summary>
        protected override void OnInitialized()
        {
            Language.OnChange += OnLanguageChanged;
        }

        /// <summary>
        /// Unsubscribe from language change notifications.
        /// </summary>
        public void Dispose()
        {
            Language.OnChange -= OnLanguageChanged;
        }

        /// <summary>
        /// Trigger UI refresh when the language changes.
        /// </summary>
        private void OnLanguageChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Resolve localized string for the current component using a derived RESX base name.
        /// </summary>
        protected string T(string key)
        {
            if (string.IsNullOrEmpty(key))
                return key;

            var baseName = BuildResourceBaseName();
            return _resolver!.Resolve(key, baseName);
        }

        /// <summary>
        /// Builds RESX base name as: {Namespace}.{TypeName}.
        /// Override to change convention.
        /// </summary>
        protected virtual string BuildResourceBaseName()
        {
            var type = GetType();
            var ns = type.Namespace ?? string.Empty;
            var typeName = type.Name;
            return string.IsNullOrEmpty(ns) ? typeName : $"{ns}.{typeName}";
        }

        /// <summary>
        /// Allow setting resolver manually (useful for tests).
        /// </summary>
        public void SetResolver(ILocalizationResolver resolver) => Resolver = resolver;
    }
}
