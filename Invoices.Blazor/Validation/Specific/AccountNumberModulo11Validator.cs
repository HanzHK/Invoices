using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoices.Blazor.Validation.Specific
{
    public class AccountNumberModulo11Validator
    {
        private readonly IStringLocalizer _localizer;
        private readonly FormFieldBlurTracker _blurTracker;

        public static AccountNumberModulo11Validator Create(
            IStringLocalizer localizer,
            FormFieldBlurTracker blurTracker)
        {
            return new AccountNumberModulo11Validator(localizer, blurTracker);
        }

        private AccountNumberModulo11Validator(
            IStringLocalizer localizer,
            FormFieldBlurTracker blurTracker)
        {
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
            _blurTracker = blurTracker ?? throw new ArgumentNullException(nameof(blurTracker));
        }

        /// <summary>
        /// Creates an asynchronous validator that validates Czech bank account number (postfix, part after dash)
        /// using modulo 11 according to Czech banking rules, but only after the field has been blurred.
        /// Uses field-specific localized message if available, otherwise falls back to a generic InvalidModulo11 message.
        /// </summary>
        /// <param name="fieldIdentifier">
        /// Unique identifier of the field (e.g. "AccountNumber"). Used for blur tracking and localization key ({fieldIdentifier}InvalidModulo11).
        /// </param>
        /// <returns>
        /// Async validator delegate returning an empty sequence if not blurred yet or value is empty,
        /// or a single localized error message when invalid after blur.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> ValidateAfterBlur(string fieldIdentifier)
        {
            return value =>
            {
                if (!_blurTracker.IsBlurred(fieldIdentifier))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                if (string.IsNullOrWhiteSpace(value))
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                if (!IsValidAccountNumberModulo11(value))
                {
                    var specific = _localizer[$"{fieldIdentifier}InvalidModulo11"];
                    var message = specific.ResourceNotFound
                        ? _localizer["InvalidModulo11"].Value
                        : specific.Value;

                    return Task.FromResult<IEnumerable<string>>(new[] { message });
                }

                return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());
            };
        }

        /// <summary>
        /// Checks if the given account number (postfix, part after dash) is valid according to Czech modulo 11 rules.
        /// Rules:
        /// - Only digits allowed.
        /// - Length 2–10 (typically 6–10).
        /// - Weights 1–10 from right to left.
        /// - Sum(digit * weight) % 11 must be:
        ///   - for length 6: 0 or 1
        ///   - for length 7–10: 0, 1 or 10
        /// </summary>
        private static bool IsValidAccountNumberModulo11(string accountNumber)
        {
            if (!accountNumber.All(char.IsDigit))
                return false;

            int length = accountNumber.Length;

            if (length < 2 || length > 10)
                return false;

            // Weights 1–10 from right to left
            int[] weights = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int sum = 0;

            for (int i = 0; i < length; i++)
            {
                int digit = accountNumber[length - 1 - i] - '0';
                int weight = weights[i];
                sum += digit * weight;
            }

            int mod = sum % 11;

            if (length == 6)
                return mod == 0 || mod == 1;

            // 7–10 digits
            return mod == 0 || mod == 1 || mod == 10;
        }
    }
}