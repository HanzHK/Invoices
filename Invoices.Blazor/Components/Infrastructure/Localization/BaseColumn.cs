using Invoices.Blazor.Services.Localization;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Infrastructure.Localization
{
    /// <summary>
    /// Base class for all column components that require localized titles.
    /// </summary>
    /// <remarks>
    /// This class provides a generic mechanism for resolving localized strings
    /// from module-specific RESX files. It automatically derives the RESX base name
    /// from the component's namespace, assuming the following folder structure:
    ///
    /// Components/&lt;Module&gt;/&lt;Feature&gt;/Columns/
    /// Components/&lt;Module&gt;/&lt;Feature&gt;/Localization/Columns.resx
    ///
    /// Example:
    /// Component namespace:
    ///     Invoices.Blazor.Components.Person.List.Columns
    ///
    /// Derived RESX base name:
    ///     Invoices.Blazor.Components.Person.List.Localization.Columns
    ///
    /// This keeps localization modular, colocated with each feature module,
    /// and avoids the need for feature-specific resolver implementations.
    /// </remarks>
    public abstract class BaseColumn : ComponentBase
    {
        private ILocalizationResolver? _resolver;

        /// <summary>
        /// Injected localization resolver. Assigned after component construction.
        /// </summary>
        [Inject]
        protected ILocalizationResolver Resolver
        {
            get => _resolver!;
            set => _resolver = value;
        }

        /// <summary>
        /// Resolves a localized string for the given key using the component's
        /// automatically derived RESX base name.
        /// </summary>
        /// <param name="key">The lookup key inside the RESX file.</param>
        /// <returns>The localized string, or the key itself if not found.</returns>
        protected string T(string key)
        {
            var baseName = BuildResourceBaseName();
            return _resolver!.Resolve(key, baseName);
        }

        /// <summary>
        /// Builds the fully qualified RESX base name based on the component's namespace.
        /// </summary>
        /// <remarks>
        /// The method replaces the trailing ".Columns" namespace segment with
        /// ".Localization.Columns", matching the expected folder structure.
        /// </remarks>
        private string BuildResourceBaseName()
        {
            var ns = GetType().Namespace!;

            if (ns.EndsWith(".Columns"))
                ns = ns[..^(".Columns".Length)] + ".Localization.Columns";

            return ns;
        }
    }
}
