using Invoices.Blazor.Validation.Infrastructure;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a validation rule that checks whether the input contains exactly
    /// the specified number of digits, but only after the field has been blurred.
    /// </summary>
    internal class DigitsExactLengthRule
    {
        private readonly MessageResolver _messages;
        private readonly FormFieldBlurTracker _blurTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DigitsExactLengthRule"/> class.
        /// </summary>
        /// <param name="messages">
        /// Service used to resolve localized validation messages using
        /// component‑specific and fallback resource keys.
        /// </param>
        /// <param name="blurTracker">
        /// Tracks blur state of form fields to determine when validation should run.
        /// </param>
        public DigitsExactLengthRule(MessageResolver messages, FormFieldBlurTracker blurTracker)
        {
            _messages = messages;
            _blurTracker = blurTracker;
        }

        /// <summary>
        /// Creates an asynchronous validator that checks whether the input contains
        /// exactly the specified number of digits, but only after the field has been blurred.
        /// </summary>
        /// <param name="fieldKey">
        /// Key used to resolve the localized error message
        /// (e.g. "Telephone" → "TelephoneDigitsExactLength").
        /// </param>
        /// <param name="exactDigits">
        /// Required number of digits the input must contain after formatting is removed.
        /// </param>
        /// <returns>
        /// A function that returns an asynchronous sequence of validation errors.
        /// If the field has not been blurred yet, or the value is empty, the validator yields no errors.
        /// Once blurred, it returns a single localized error message when the digit count does not match,
        /// otherwise an empty sequence.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldKey, int exactDigits)
        {
            return value =>
            {
                if (_blurTracker == null || !_blurTracker.IsBlurred(fieldKey))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                if (string.IsNullOrWhiteSpace(value))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                var digitsOnly = new string(value!.Where(char.IsDigit).ToArray());

                if (digitsOnly.Length != exactDigits)
                {
                    var message = _messages.Resolve(fieldKey, "DigitsExactLength", exactDigits);
                    return Task.FromResult<IEnumerable<string>>(new[] { message });
                }

                return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());
            };
        }
    }
}
