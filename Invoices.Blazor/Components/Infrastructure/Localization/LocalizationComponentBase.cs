using Invoices.Blazor.Services.Localization;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Base class for components that require localized strings.
    /// </summary>
    /// <remarks>
    /// Derives the RESX base name automatically from the component's
    /// namespace + type name (e.g. Invoices.Blazor.Components.Person.List.PersonList).
    /// Place a RESX named <TypeName>.resx next to the component to localize it.
    /// </remarks>
    public abstract class LocalizationComponentBase : ComponentBase
    {
        private ILocalizationResolver? _resolver;

        [Inject]
        protected ILocalizationResolver Resolver
        {
            get => _resolver!;
            set => _resolver = value;
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
        /// Builds RESX base name as: {Namespace}.{TypeName}
        /// (e.g. Invoices.Blazor.Components.Person.List.PersonList for PersonList component).
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
