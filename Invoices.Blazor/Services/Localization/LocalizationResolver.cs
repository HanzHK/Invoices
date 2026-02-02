using System.Resources;

namespace Invoices.Blazor.Services.Localization
{
    /// <summary>
    /// Default implementation of <see cref="ILocalizationResolver"/> that loads
    /// localized strings from module-specific RESX files.
    /// </summary>
    /// <remarks>
    /// This resolver is intentionally generic. It does not infer module or component
    /// structure. The caller must supply the fully qualified RESX base name, enabling
    /// each feature module to maintain its own localization files without requiring
    /// feature-specific resolver implementations.
    /// </remarks>
    public class LocalizationResolver : ILocalizationResolver
    {
        /// <inheritdoc />
        public string Resolve(string key, string resourceBaseName)
        {
            var manager = new ResourceManager(resourceBaseName, typeof(LocalizationResolver).Assembly);

            var value = manager.GetString(key);
            return string.IsNullOrEmpty(value) ? key : value;
        }
    }
}
