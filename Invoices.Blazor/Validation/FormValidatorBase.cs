using Invoices.Blazor.Validation.Infrastructure;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoices.Blazor.Validation
{
    /// <summary>
    /// Provides shared infrastructure for form validators, including access to
    /// localized resources and blur tracking. Concrete validators supply the
    /// primary localizer (typically bound to the hosting component) and an
    /// optional fallback localizer (e.g. for generic validation messages).
    /// </summary>
    public abstract class FormValidatorBase
    {
        protected readonly IStringLocalizer Primary;
        protected readonly IStringLocalizer Fallback;
        protected readonly FormFieldBlurTracker BlurTracker;

        private protected readonly MessageResolver Messages;

        /// <summary>
        /// Registry of validators grouped by field name.
        /// Each field may have one or more validators (Required, Length, etc.).
        /// </summary>
        private protected readonly Dictionary<string, List<Func<object?, Task<IEnumerable<string>>>>> FieldValidators
            = new();

        /// <summary>
        /// Stores the last known validation errors for each field.
        /// Used when validation is triggered manually (e.g. MudSelect blur).
        /// </summary>
        private protected readonly Dictionary<string, IEnumerable<string>> FieldErrors
            = new();

        protected FormValidatorBase(
            IStringLocalizer primary,
            IStringLocalizer fallback,
            FormFieldBlurTracker blurTracker)
        {
            Primary = primary;
            Fallback = fallback;
            BlurTracker = blurTracker;
            Messages = new MessageResolver(primary, fallback);
        }

        /// <summary>
        /// Registers a validator for a specific field.
        /// Called by derived classes (FormValidator) when creating rule delegates.
        /// </summary>
        private protected void RegisterValidator(
            string fieldName,
            Func<object?, Task<IEnumerable<string>>> validator)
        {
            if (!FieldValidators.TryGetValue(fieldName, out var list))
            {
                list = new List<Func<object?, Task<IEnumerable<string>>>>();
                FieldValidators[fieldName] = list;
            }

            list.Add(validator);
        }

        /// <summary>
        /// Executes all validators registered for the specified field.
        /// Stores the resulting errors for later retrieval.
        /// </summary>
        /// <param name="fieldName">Name of the field to validate.</param>
        /// <param name="value">Current value of the field.</param>
        /// <returns>Validation errors for the field.</returns>
        public async Task<IEnumerable<string>> ValidateFieldAsync(string fieldName, object? value)
        {
            if (!FieldValidators.TryGetValue(fieldName, out var validators))
                return Enumerable.Empty<string>();

            var errors = new List<string>();

            foreach (var validator in validators)
            {
                var result = await validator(value);
                errors.AddRange(result);
                if (errors.Count > 0)
                    break;
            }

            FieldErrors[fieldName] = errors;
            return errors;
        }

        /// <summary>
        /// Returns the last known validation errors for the specified field.
        /// </summary>
        public IEnumerable<string> GetErrors(string fieldName)
            => FieldErrors.TryGetValue(fieldName, out var e) ? e : Enumerable.Empty<string>();

        protected IStringLocalizer Localizer => Primary;
    }
}
