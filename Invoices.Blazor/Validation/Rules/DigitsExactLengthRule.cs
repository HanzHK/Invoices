using Invoices.Blazor.Validation.Infrastructure;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a validation rule that checks whether a model property contains exactly
    /// the specified number of digits. This validator evaluates the processed model value
    /// rather than the raw input, ensuring validation runs after any input handlers have
    /// formatted or transformed the data.
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
        /// Creates an asynchronous validator that checks whether the model property contains
        /// exactly the specified number of digits. Validation evaluates the model value after
        /// any input handlers have processed it, ensuring accurate validation of the final state.
        /// Validation is triggered when the field has been blurred or when the user has entered
        /// the required number of digits.
        /// </summary>
        /// <param name="getModelValue">
        /// Function that retrieves the current value from the model property being validated.
        /// </param>
        /// <param name="fieldKey">
        /// Key used to resolve the localized error message
        /// (e.g. "Telephone" → "TelephoneDigitsExactLength").
        /// </param>
        /// <param name="exactDigits">
        /// Required number of digits the model value must contain after formatting is removed.
        /// </param>
        /// <returns>
        /// A function that returns an asynchronous sequence of validation errors.
        /// The validator ignores the input parameter and evaluates the model value directly.
        /// If the field has not been blurred yet and the user has not reached the required
        /// number of digits, the validator yields no errors. Once blurred or once the required
        /// digit count is reached, it returns a single localized error message when the digit
        /// count does not match, otherwise an empty sequence.
        /// </returns>
        public Func<object, Task<IEnumerable<string>>> Create(
            Func<string?> getModelValue,
            string fieldKey,
            int exactDigits)
        {
            return _ =>
            {
                var modelValue = getModelValue();

                if (string.IsNullOrWhiteSpace(modelValue))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                var digitsOnly = new string(modelValue!.Where(char.IsDigit).ToArray());

                bool shouldValidate =
                    _blurTracker.IsBlurred(fieldKey) ||
                    digitsOnly.Length >= exactDigits;

                if (!shouldValidate)
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

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