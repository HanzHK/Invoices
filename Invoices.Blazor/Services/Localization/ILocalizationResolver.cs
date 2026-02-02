namespace Invoices.Blazor.Services.Localization
{
    /// <summary>
    /// Provides localized strings from module-specific RESX files.
    /// </summary>
    /// <remarks>
    /// The caller supplies both the lookup key and the fully qualified RESX base name.
    /// This enables a modular localization structure where each feature maintains its
    /// own resource files without requiring feature-specific resolver implementations.
    /// </remarks>
    public interface ILocalizationResolver
    {
        /// <summary>
        /// Resolves a localized string from a specific RESX resource file.
        /// </summary>
        /// <param name="key">The lookup key inside the RESX file.</param>
        /// <param name="resourceBaseName">
        /// Fully qualified base name of the RESX file (e.g.
        /// "Invoices.Blazor.Components.Person.List.Localization.Columns").
        /// </param>
        /// <returns>
        /// The localized string if found; otherwise the key itself.
        /// </returns>
        string Resolve(string key, string resourceBaseName);
    }
}
