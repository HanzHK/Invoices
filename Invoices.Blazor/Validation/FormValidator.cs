using Invoices.Blazor.Validation.Rules;
using Invoices.Shared.Models.Common;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Invoices.Blazor.Validation
{
    /// <summary>
    /// Provides a collection of reusable validation rules for Blazor forms.
    /// This validator acts as an orchestrator: it composes rule classes,
    /// supplies them with localization and blur‑tracking services, and exposes
    /// a convenient API for use in components.
    /// </summary>
    public class FormValidator : FormValidatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormValidator"/> class.
        /// </summary>
        /// <param name="factory">
        /// Factory used to create localizers for the hosting component and for
        /// the shared validator resources.
        /// </param>
        /// <param name="blurTracker">
        /// Service responsible for tracking blur state of individual form fields,
        /// enabling validation rules that should only execute after the user
        /// leaves a field.
        /// </param>
        /// <param name="componentType">
        /// Type of the hosting component whose resource file provides
        /// component‑specific validation messages (e.g. PersonForm, InvoiceForm).
        /// </param>
        public FormValidator(
            IStringLocalizerFactory factory,
            FormFieldBlurTracker blurTracker,
            Type componentType)
            : base(
                factory.Create(componentType),          
                factory.Create(typeof(FormValidator)),  // Fallback localizer, for generic messages
                blurTracker)
        {
        }

        /// <summary>
        /// Creates a validator ensuring that a value is present.
        /// This generic overload supports reference types and nullable value types,
        /// and delegates the actual validation logic to <see cref="RequiredRule"/>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the value being validated. When <typeparamref name="T"/> is
        /// <see cref="string"/>, additional whitespace checks are applied.
        /// </typeparam>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameRequired").
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<T?, Task<IEnumerable<string>>> Required<T>(string fieldName)
        {
            return new RequiredRule(Messages).Create<T>(fieldName);
        }

        /// <summary>
        /// Convenience overload for validating string fields.
        /// Equivalent to calling <c>Required&lt;string&gt;(fieldName)</c>.
        /// </summary>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameRequired").
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Required(string fieldName)
        {
            return new RequiredRule(Messages).Create(fieldName);
        }

        /// <summary>
        /// Creates a validator ensuring that the input value has exactly the specified
        /// length. Delegates the validation logic to <see cref="LengthRule"/>, which
        /// handles localized message resolution.
        /// </summary>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "PostalCode" → "PostalCodeLength").
        /// </param>
        /// <param name="length">
        /// Required number of characters the input must contain.
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Length(string fieldName, int length)
        {
            return new LengthRule(Messages).Create(fieldName, length);
        }

        /// <summary>
        /// Creates a validator ensuring that the input value matches a specified
        /// formatting pattern once the required number of digits has been entered.
        /// Delegates the validation logic to <see cref="FormatRule"/>, which handles
        /// localized message resolution.
        /// </summary>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Telephone" → "TelephoneFormat").
        /// </param>
        /// <param name="pattern">
        /// Regular expression pattern the formatted value must satisfy once validation
        /// is triggered.
        /// </param>
        /// <param name="requiredLength">
        /// Minimum number of digits (ignoring formatting characters) required before
        /// the pattern check is applied.
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> Format(string fieldName, string pattern, int requiredLength)
        {
            return new FormatRule(Messages).Create(fieldName, pattern, requiredLength);
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
        /// Creates a validator ensuring that the input contains exactly the specified
        /// number of digits, but only after the field has been blurred.
        /// </summary>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Telephone" → "TelephoneDigitsExactLength").
        /// </param>
        /// <param name="exactDigits">
        /// Required number of digits the input must contain.
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> DigitsExactLengthAfterBlur(string fieldName, int exactDigits)
        {
            return new DigitsExactLengthRule(Messages, BlurTracker).Create(fieldName, exactDigits);
        }

        /// <summary>
        /// Creates a validator ensuring that the input value does not exceed the
        /// specified maximum length. Delegates the validation logic to
        /// <see cref="MaxLengthRule"/>, which handles localized message resolution.
        /// </summary>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameMaxLength").
        /// </param>
        /// <param name="maxLength">
        /// Maximum allowed number of characters.
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> MaxLength(string fieldName, int maxLength)
        {
            return new MaxLengthRule(Messages).Create(fieldName, maxLength);
        }

        /// <summary>
        /// Creates a validator ensuring that the input value meets the specified
        /// minimum length. Delegates the validation logic to <see cref="MinLengthRule"/>,
        /// which handles localized message resolution.
        /// </summary>
        /// <param name="fieldName">
        /// Resource key prefix used to resolve the localized error message
        /// (e.g. "Name" → "NameMinLength").
        /// </param>
        /// <param name="minLength">
        /// Minimum required number of characters.
        /// </param>
        /// <returns>
        /// A function that asynchronously returns validation errors for the field.
        /// </returns>
        public Func<string?, Task<IEnumerable<string>>> MinLength(string fieldName, int minLength)
        {
            return new MinLengthRule(Messages).Create(fieldName, minLength);
        }

    }
}
