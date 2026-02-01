using Invoices.Blazor.Validation.Infrastructure;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides a reusable validation rule that checks whether the input value
    /// meets a specified minimum length. Empty or null values are considered
    /// valid; use <c>Required</c> to enforce non‑emptiness.
    /// </summary>
    internal class MinLengthRule
    {
        private readonly MessageResolver _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinLengthRule"/> class.
        /// </summary>
        /// <param name="messages">
        /// Service used to resolve localized validation messages using
        /// component‑specific and fallback resource keys.
        /// </param>
        public MinLengthRule(MessageResolver messages)
        {
            _messages = messages;
        }

        /// <summary>
        /// Creates an asynchronous validation rule ensuring that the input value
        /// meets the specified minimum length.
        /// </summary>
        /// <param name="fieldName">
        /// Name of the field used to construct the localization key
        /// (<c>{fieldName}MinLength</c>).
        /// </param>
        /// <param name="minLength">
        /// Minimum required number of characters.
        /// </param>
        /// <returns>
        /// A validation function returning an empty sequence when valid, or a
        /// sequence containing one localized error message when the value is
        /// shorter than the required length.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldName, int minLength)
        {
            return value =>
            {
                if (string.IsNullOrEmpty(value))
                    return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());

                if (value.Length < minLength)
                {
                    var message = _messages.Resolve(fieldName, "MinLength", minLength);
                    return Task.FromResult<IEnumerable<string>>(new[] { message });
                }

                return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());
            };
        }
    }
}
