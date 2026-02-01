using Invoices.Blazor.Validation.Infrastructure;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a reusable validation rule that checks whether the input value
    /// has exactly the specified length. Empty or null values are considered
    /// valid; use <c>Required</c> to enforce non‑emptiness.
    /// </summary>
    internal class LengthRule
    {
        private readonly MessageResolver _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="LengthRule"/> class.
        /// </summary>
        /// <param name="messages">
        /// Service used to resolve localized validation messages using
        /// component‑specific and fallback resource keys.
        /// </param>
        public LengthRule(MessageResolver messages)
        {
            _messages = messages;
        }

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
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldKey, int length)
        {
            return value =>
            {
                if (string.IsNullOrWhiteSpace(value) || (value?.Length ?? 0) == length)
                    return Task.FromResult<IEnumerable<string>>(Enumerable.Empty<string>());

                var message = _messages.Resolve(fieldKey, "Length", length);
                return Task.FromResult<IEnumerable<string>>(new[] { message });
            };
        }
    }
}
