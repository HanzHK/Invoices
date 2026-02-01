using Microsoft.Extensions.Localization;

namespace Invoices.Blazor.Validation.Infrastructure
{
    /// <summary>
    /// Provides centralized logic for resolving localized validation messages,
    /// supporting both component‑specific resource keys and generic fallback
    /// keys shared across all validators.
    /// </summary>
    internal class MessageResolver
    {
        private readonly IStringLocalizer _primary;
        private readonly IStringLocalizer _fallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageResolver"/> class,
        /// using the supplied primary and fallback localizers for message lookup.
        /// </summary>
        /// <param name="primary">
        /// Localizer used to resolve component‑specific validation messages
        /// (e.g. PersonForm.resx).
        /// </param>
        /// <param name="fallback">
        /// Localizer used to resolve generic validation messages when a
        /// component‑specific key is not found.
        /// </param>
        public MessageResolver(IStringLocalizer primary, IStringLocalizer fallback)
        {
            _primary = primary;
            _fallback = fallback;
        }

        /// <summary>
        /// Resolves a localized validation message using a two‑level lookup:
        /// first attempting a component‑specific key (<c>{fieldName}{ruleKey}</c>),
        /// and falling back to a generic key (<c>{ruleKey}</c>) when necessary.
        /// </summary>
        /// <param name="fieldName">
        /// Name of the validated field, used to construct the component‑specific
        /// resource key.
        /// </param>
        /// <param name="ruleKey">
        /// Base name of the validation rule (e.g. <c>MinLength</c>,
        /// <c>MaxLength</c>, <c>Format</c>).
        /// </param>
        /// <param name="args">
        /// Optional formatting arguments passed to the localized message.
        /// </param>
        /// <returns>
        /// The resolved localized validation message.
        /// </returns>
        public string Resolve(string fieldName, string ruleKey, params object[] args)
        {
            var specific = _primary[$"{fieldName}{ruleKey}", args];
            if (!specific.ResourceNotFound)
                return specific.Value;

            var generic = _fallback[ruleKey, args];
            return generic.Value;
        }
    }
}
