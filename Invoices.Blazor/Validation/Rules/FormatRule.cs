using System.Text.RegularExpressions;
using Invoices.Blazor.Validation.Infrastructure;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a reusable validation rule that checks whether the input value
    /// matches a specified formatting pattern once the required number of digits
    /// has been entered.
    /// </summary>
    internal class FormatRule
    {
        private readonly MessageResolver _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatRule"/> class.
        /// </summary>
        /// <param name="messages">
        /// Service used to resolve localized validation messages using
        /// component‑specific and fallback resource keys.
        /// </param>
        public FormatRule(MessageResolver messages)
        {
            _messages = messages;
        }

        /// <summary>
        /// Creates an asynchronous validator that checks whether the input matches a specified
        /// formatting pattern once the required number of digits has been entered.
        /// </summary>
        /// <param name="fieldKey">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Telephone" → "TelephoneFormat").
        /// </param>
        /// <param name="pattern">
        /// Regular expression pattern the formatted value must satisfy once validation is triggered.
        /// </param>
        /// <param name="requiredLength">
        /// Minimum number of digits (ignoring formatting characters) required before the pattern check is applied.
        /// </param>
        /// <returns>
        /// A function that returns an asynchronous sequence of validation errors.
        /// If the value is empty or contains fewer digits than required, no errors are returned.
        /// Once the digit threshold is met, the validator checks the pattern and yields a single
        /// localized error message if the format is invalid; otherwise an empty sequence.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldKey, string pattern, int requiredLength)
        {
            return value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                var digitsOnly = new string(value!.Where(char.IsDigit).ToArray());

                if (digitsOnly.Length < requiredLength)
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
