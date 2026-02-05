using System.Text.RegularExpressions;
using Invoices.Blazor.Validation.Infrastructure;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a reusable validation rule that checks whether the input value
    /// matches a specified formatting pattern. Validation is triggered only when
    /// the field has been blurred or when the user has already entered the required
    /// number of digits. Formatting characters are ignored when determining whether
    /// validation should run.
    /// </summary>
    internal class FormatRule
    {
        private readonly MessageResolver _messages;
        private readonly FormFieldBlurTracker _blurTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatRule"/> class.
        /// </summary>
        /// <param name="messages">
        /// Service used to resolve localized validation messages using
        /// component‑specific and fallback resource keys.
        /// </param>
        /// <param name="blurTracker">
        /// Tracks blur state of form fields to determine when validation should run.
        /// </param>
        public FormatRule(MessageResolver messages, FormFieldBlurTracker blurTracker)
        {
            _messages = messages;
            _blurTracker = blurTracker;
        }

        /// <summary>
        /// Creates an asynchronous validator that checks whether the input matches a specified
        /// formatting pattern. Validation is performed only when the field has been blurred or
        /// when the user has already entered the required number of digits.
        /// </summary>
        /// <param name="fieldKey">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Telephone" → "TelephoneFormat").
        /// </param>
        /// <param name="pattern">
        /// Regular expression pattern the formatted value must satisfy once validation is triggered.
        /// </param>
        /// <param name="requiredLength">
        /// Number of digits (ignoring formatting characters) required before the pattern check is applied.
        /// </param>
        /// <returns>
        /// A function that returns an asynchronous sequence of validation errors.
        /// If the field has not been blurred yet and the user has not reached the required
        /// number of digits, the validator yields no errors. Once blurred or once the required
        /// digit count is reached, it returns a single localized error message when the pattern
        /// does not match, otherwise an empty sequence.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldKey, string pattern, int requiredLength)
        {
            return value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                var digitsOnly = new string(value!.Where(char.IsDigit).ToArray());

                // SMART VALIDATION:
                // - validate on blur
                // - OR when user reached exact digit count
                bool shouldValidate =
                    _blurTracker.IsBlurred(fieldKey) ||
                    digitsOnly.Length >= requiredLength;

                if (!shouldValidate)
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                if (!Regex.IsMatch(value, pattern))
                {
                    var message = _messages.Resolve(fieldKey, "Format");
                    return Task.FromResult<IEnumerable<string>>(new[] { message });
                }

                return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());
            };
        }
    }
}
