using Invoices.Blazor.Validation.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Invoices.Blazor.Validation.Rules
{
    /// <summary>
    /// Provides validation rules ensuring that a value is present.
    /// Supports both generic reference types and a string‑specific variant
    /// that additionally checks for empty or whitespace‑only input.
    /// </summary>
    internal class RequiredRule
    {
        private readonly MessageResolver _messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredRule"/> class.
        /// </summary>
        /// <param name="messages">
        /// Service used to resolve localized validation messages using
        /// component‑specific and fallback resource keys.
        /// </param>
        internal RequiredRule(MessageResolver messages)
        {
            _messages = messages;
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
        public Func<T?, Task<IEnumerable<string>>> Create<T>(string fieldKey)
        {
            return value =>
            {
                bool isInvalid =
                    value is null ||
                    (value is string s && string.IsNullOrWhiteSpace(s));

                if (isInvalid)
                {
                    var message = _messages.Resolve(fieldKey, "Required");
                    return Task.FromResult<IEnumerable<string>>(new[] { message });
                }

                return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());
            };
        }

        /// <summary>
        /// Convenience overload for validating string fields.
        /// This method simply forwards the call to the generic
        /// <see cref="Create{T}(string)"/> implementation with
        /// <typeparamref name="T"/> set to <see cref="string"/>.
        /// </summary>
        /// <param name="fieldKey">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameRequired").
        /// </param>
        /// <returns>
        /// A validator function equivalent to calling
        /// <c>Create&lt;string&gt;(fieldKey)</c>.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Create(string fieldKey)
            => Create<string>(fieldKey);
    }
}
