using System.Globalization;
using System.Text;

namespace Invoices.Blazor.InputHandlers
{
    public enum TextCasing
    {
        None,
        Lower,
        Upper,
        Title
    }

    /// <summary>
    /// Configuration options controlling how raw text input is normalized
    /// before validation or persistence.
    /// </summary>
    public sealed class TextInputOptions
    {
        /// <summary>
        /// When enabled, leading and trailing whitespace is removed
        /// before any other transformations are applied.
        /// </summary>
        public bool Trim { get; set; } = true;

        /// <summary>
        /// When enabled, all whitespace sequences (spaces, tabs, newlines)
        /// are collapsed into a single space. Useful for normalizing names,
        /// addresses and other free‑text fields.
        /// </summary>
        public bool CollapseWhitespace { get; set; } = true;

        /// <summary>
        /// When enabled, diacritic marks are removed from characters
        /// (e.g. "á" → "a"). Intended for scenarios requiring ASCII‑normalized
        /// text. Disabled by default to preserve Czech diacritics.
        /// </summary>
        public bool RemoveDiacritics { get; set; } = false;

        /// <summary>
        /// Specifies the casing rule applied to the normalized text.
        /// Supports lower‑case, upper‑case and culture‑aware title case.
        /// </summary>
        public TextCasing Casing { get; set; } = TextCasing.None;

        /// <summary>
        /// Maximum allowed length of the processed text. If null,
        /// no truncation is applied. Truncation occurs after all
        /// other transformations (trim, whitespace, casing, diacritics).
        /// </summary>
        public int? MaxLength { get; set; }
    }


    public static class TextInputHandler
    {
        /// <summary>
        /// Normalizes raw text input according to the specified options.
        /// Intended for use in MudBlazor text fields to ensure consistent,
        /// predictable formatting before validation or persistence.
        /// </summary>
        /// <param name="value">
        /// Raw user input. If <c>null</c> or empty, an empty string is returned.
        /// </param>
        /// <param name="options">
        /// Configuration controlling trimming, whitespace normalization,
        /// casing rules and optional diacritic removal.
        /// </param>
        /// <returns>
        /// Normalized text after applying the selected transformations.
        /// </returns>
        /// <remarks>
        /// This method performs only input transformation, not validation.
        /// It is safe to call on every keystroke.
        /// </remarks>
        public static string Handle(string? value, TextInputOptions options)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var result = value;

            if (options.Trim)
                result = result.Trim();

            if (options.CollapseWhitespace)
                result = CollapseWhitespace(result);

            if (options.RemoveDiacritics)
                result = RemoveDiacritics(result);

            result = ApplyCasing(result, options.Casing);
            result = ApplyMaxLength(result, options.MaxLength);

            return result;
        }
        /// <summary>
        /// Replaces all sequences of whitespace characters with a single space.
        /// Tabs, newlines and multiple spaces are normalized to one space.
        /// </summary>
        /// <param name="value">
        /// Input text to normalize. Must not be null.
        /// </param>
        /// <returns>
        /// Text with collapsed whitespace. Leading and trailing whitespace
        /// is preserved or removed depending on the caller (typically trimmed).
        /// </returns>
        private static string CollapseWhitespace(string value)
        {
            var result = new StringBuilder(value.Length);

            bool previousWasWhitespace = false;

            foreach (char c in value)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!previousWasWhitespace)
                    {
                        result.Append(' ');
                        previousWasWhitespace = true;
                    }
                }
                else
                {
                    result.Append(c);
                    previousWasWhitespace = false;
                }
            }

            return result.ToString();
        }
        /// <summary>
        /// Removes diacritic marks from characters (e.g. "á" → "a").
        /// Intended for scenarios where normalized ASCII text is required,
        /// such as generating aliases or identifiers.
        /// </summary>
        /// <param name="value">
        /// Input text to normalize. Must not be null.
        /// </param>
        /// <returns>
        /// Text with diacritic marks removed. Characters without diacritics
        /// remain unchanged.
        /// </returns>
        private static string RemoveDiacritics(string value)
        {
            var normalized = value.Normalize(NormalizationForm.FormD);
            var result = new StringBuilder(normalized.Length);

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

                // Skip non-spacing marks (diacritics)
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    result.Append(c);
            }

            return result.ToString().Normalize(NormalizationForm.FormC);
        }
        /// <summary>
        /// Applies the selected casing rule to the input text.
        /// Supports lower‑case, upper‑case and culture‑aware title case.
        /// </summary>
        /// <param name="value">
        /// Input text to transform. Must not be null.
        /// </param>
        /// <param name="casing">
        /// Casing rule to apply. If <see cref="TextCasing.None"/> is used,
        /// the input is returned unchanged.
        /// </param>
        /// <returns>
        /// Text transformed according to the specified casing rule.
        /// </returns>
        private static string ApplyCasing(string value, TextCasing casing)
        {
            switch (casing)
            {
                case TextCasing.Lower:
                    return value.ToLowerInvariant();

                case TextCasing.Upper:
                    return value.ToUpperInvariant();

                case TextCasing.Title:
                    return CultureInfo.GetCultureInfo("cs-CZ")
                                      .TextInfo
                                      .ToTitleCase(value.ToLower());

                default:
                    return value;
            }
        }
        /// <summary>
        /// Truncates the input text to the specified maximum length.
        /// If <paramref name="maxLength"/> is null or less than 1,
        /// the input is returned unchanged.
        /// </summary>
        /// <param name="value">
        /// Input text to truncate. Must not be null.
        /// </param>
        /// <param name="maxLength">
        /// Maximum allowed length of the returned text. If null,
        /// no truncation is applied.
        /// </param>
        /// <returns>
        /// Text truncated to the specified maximum length, or the original
        /// text if no truncation is required.
        /// </returns>
        private static string ApplyMaxLength(string value, int? maxLength)
        {
            if (maxLength is null || maxLength < 1)
                return value;

            return value.Length <= maxLength
                ? value
                : value.Substring(0, maxLength.Value);
        }



    }
}
