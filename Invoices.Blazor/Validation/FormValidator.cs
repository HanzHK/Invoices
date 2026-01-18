using Invoices.Shared.Models.Common;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoices.Blazor.Validation
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormValidator"/> class,
    /// providing access to localized validation messages and blur‑tracking logic
    /// used by validators that depend on field focus state.
    /// </summary>
    /// <param name="localizer">
    /// Localizer used to resolve validation message resources.
    /// </param>
    /// <param name="blurTracker">
    /// Service responsible for tracking whether individual form fields
    /// have been blurred, enabling validators that should only run after
    /// the user leaves a field.
    /// </param>
    public class FormValidator
    {
        private readonly IStringLocalizer<FormValidator> L;
        private readonly FormFieldBlurTracker _blurTracker;

        public FormValidator(
            IStringLocalizer<FormValidator> localizer,
            FormFieldBlurTracker blurTracker)
        {
            L = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _blurTracker = blurTracker ?? throw new ArgumentNullException(nameof(blurTracker));
        }

        /// <summary>
        /// Creates an asynchronous validator that ensures the input value is present.
        /// For reference types and nullable value types, the value must not be <c>null</c>.
        /// When <typeparamref name="T"/> is <see cref="string"/>, the value must also not be
        /// empty or consist solely of whitespace.
        /// </summary>
        /// <param name="fieldKey">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameRequired").
        /// </param>
        /// <returns>
        /// A function that returns an asynchronous sequence of validation errors.
        /// If the value is considered missing according to the rules above, a single
        /// localized error message is returned; otherwise an empty sequence.
        /// </returns>

        public Func<T?, Task<IEnumerable<string>>> Required<T>(string fieldKey)
        {
            return value =>
            {
                bool isInvalid =
                    value is null ||
                    (value is string s && string.IsNullOrWhiteSpace(s));

                if (isInvalid)
                {
                    string error = L[$"{fieldKey}Required"].Value
                                   ?? $"{fieldKey} is required";

                    return Task.FromResult<IEnumerable<string>>(
                        new List<string> { error }
                    );
                }

                return Task.FromResult<IEnumerable<string>>(
                    Enumerable.Empty<string>()
                );
            };
        }

        /// <summary>
        /// Convenience overload for validating string fields.
        /// This method simply forwards the call to the generic
        /// <see cref="Required{T}(string)"/> implementation with
        /// <typeparamref name="T"/> set to <see cref="string"/>.
        /// </summary>
        /// <param name="fieldKey">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameRequired").
        /// </param>
        /// <returns>
        /// A validator function equivalent to calling
        /// <c>Required&lt;string&gt;(fieldKey)</c>.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Required(string fieldKey)
            => Required<string>(fieldKey);


        /// <summary>
        /// Creates an asynchronous validator that checks whether the input value has
        /// exactly the specified length.
        /// </summary>
        /// <param name="fieldKey">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "PostalCode" → "PostalCodeLength").
        /// </param>
        /// <param name="length">
        /// Required number of characters the input must contain.
        /// </param>
        /// <returns>
        /// A function that returns an asynchronous sequence of validation errors.
        /// If the value is null, empty, or already matches the required length,
        /// the validator yields no errors. Otherwise, it returns a single localized
        /// error message indicating the expected length.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Length(string fieldKey, int length)
        {
            return value =>
            {
                if (string.IsNullOrWhiteSpace(value) || (value?.Length ?? 0) == length)
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                string error = L[$"{fieldKey}Length", length].Value ?? $"Length must be {length}";
                return Task.FromResult<IEnumerable<string>>(new List<string> { error });
            };
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
        public Func<string?, Task<IEnumerable<string>>> Format(string fieldKey, string pattern, int requiredLength)
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
                    string error = L[$"{fieldKey}Format"].Value ?? "Invalid format";
                    return Task.FromResult<IEnumerable<string>>(new List<string> { error });
                }

                return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());
            };
        }

        /// <summary>
        /// Combines multiple asynchronous validators into a single validator that executes them in order.
        /// </summary>
        /// <param name="validators">
        /// A sequence of asynchronous validation functions to be evaluated.
        /// Each validator receives the same input value and returns zero or more error messages.
        /// </param>
        /// <returns>
        /// A composed validator that runs each provided validator sequentially.
        /// It aggregates all returned error messages, but stops execution as soon as the first validator
        /// produces any errors. If no validator reports an error, an empty sequence is returned.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> ValidateAll(
            params Func<string?, Task<IEnumerable<string>>>[] validators)
        {
            return async value =>
            {
                var errors = new List<string>();

                foreach (var validator in validators)
                {
                    var result = await validator(value);
                    errors.AddRange(result);
                    if (errors.Count > 0) break; 
                }

                return errors;
            };
        }

        /// <summary>
        /// Creates an asynchronous validator that checks whether the input contains
        /// exactly the specified number of digits, but only after the field has been blurred.
        /// </summary>
        /// <param name="fieldIdentifier">
        /// Key used to resolve the localized error message (e.g. "Telephone" → "TelephoneDigitsExactLength").
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
        public Func<string?, Task<IEnumerable<string>>> DigitsExactLengthAfterBlurAsync(string fieldIdentifier, int exactDigits)
        {
            return value =>
            {
                if (_blurTracker == null || !_blurTracker.IsBlurred(fieldIdentifier))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                if (string.IsNullOrWhiteSpace(value))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                var digitsOnly = new string(value!.Where(char.IsDigit).ToArray());

                if (digitsOnly.Length != exactDigits)
                {
                    string msg = L[$"{fieldIdentifier}DigitsExactLength", exactDigits].Value
                                 ?? $"Must contain exactly {exactDigits} digits";

                    return Task.FromResult<IEnumerable<string>>(new List<string> { msg });
                }

                return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());
            };
        }

    }
}