using Microsoft.Extensions.Localization;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a reusable validation rule that checks whether the input value
    /// exceeds a specified maximum length. Empty or null values are considered
    /// valid; use <c>Required</c> to enforce non‑emptiness.
    /// 
    /// The rule supports a two‑level localization fallback:
    /// 1) Specific key from the hosting component: {fieldName}MaxLength
    /// 2) Generic key from the shared validator resources: MaxLength
    /// </summary>
    public class MaxLengthRule
    {
        private readonly IStringLocalizer _primary;
        private readonly IStringLocalizer _fallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxLengthRule"/> class.
        /// </summary>
        /// <param name="primary">
        /// Primary localizer used to resolve component‑specific validation
        /// messages (e.g. PersonForm.resx).
        /// </param>
        /// <param name="fallback">
        /// Fallback localizer used to resolve generic validation messages
        /// (e.g. FormValidator.resx).
        /// </param>
        public MaxLengthRule(IStringLocalizer primary, IStringLocalizer fallback)
        {
            _primary = primary;
            _fallback = fallback;
        }

        /// <summary>
        /// Creates an asynchronous validation rule ensuring that the input value
        /// does not exceed the specified maximum length.
        /// </summary>
        /// <param name="fieldName">
        /// Name of the field used to construct the localization key
        /// (<c>{fieldName}MaxLength</c>).
        /// </param>
        /// <param name="maxLength">
        /// Maximum allowed number of characters.
        /// </param>
        /// <returns>
        /// A validation function returning an empty sequence when valid, or a
        /// sequence containing one localized error message when the value exceeds
        /// the allowed length.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldName, int maxLength)
        {
            return value =>
            {
                if (string.IsNullOrEmpty(value))
                    return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());

                if (value.Length <= maxLength)
                    return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());

                // 1) Specific key
                var specific = _primary[$"{fieldName}MaxLength", maxLength];
                if (!specific.ResourceNotFound)
                    return Task.FromResult<IEnumerable<string>>(new[] { specific.Value });

                // 2) Generic fallback
                var generic = _fallback["MaxLength", maxLength];
                return Task.FromResult<IEnumerable<string>>(new[] { generic.Value });
            };
        }

    }
}
