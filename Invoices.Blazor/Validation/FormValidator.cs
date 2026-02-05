using Invoices.Blazor.Validation.Rules;
using Invoices.Shared.Models.Common;
using Microsoft.Extensions.Localization;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml;

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
        /// <param name="factory">Factory used to create component and fallback localizers.</param>
        /// <param name="blurTracker">Service tracking blur state of form fields.</param>
        /// <param name="componentType">Type of the hosting component for primary localization.</param>
        public FormValidator(
            IStringLocalizerFactory factory,
            FormFieldBlurTracker blurTracker,
            Type componentType)
            : base(
                factory.Create(componentType),
                factory.Create(typeof(FormValidator)),
                blurTracker)
        {
        }

        /// <summary>
        /// Creates a Required validator for nullable or reference types.
        /// Registers the validator for field‑level execution.
        /// </summary>
        public Func<T?, Task<IEnumerable<string>>> Required<T>(string fieldName)
        {
            var validator = new RequiredRule(Messages).Create<T>(fieldName);
            RegisterValidator(fieldName, v => validator((T?)v));
            return validator;
        }

        /// <summary>
        /// Creates a Required validator for string fields.
        /// Registers the validator for field‑level execution.
        /// </summary>
        public Func<string?, Task<IEnumerable<string>>> Required(string fieldName)
        {
            var validator = new RequiredRule(Messages).Create(fieldName);
            RegisterValidator(fieldName, v => validator((string?)v));
            return validator;
        }

        /// <summary>
        /// Creates a validator ensuring the input has exactly the specified length.
        /// Registers the validator for field‑level execution.
        /// </summary>
        public Func<string?, Task<IEnumerable<string>>> Length(string fieldName, int length)
        {
            var validator = new LengthRule(Messages).Create(fieldName, length);
            RegisterValidator(fieldName, v => validator((string?)v));
            return validator;
        }

        /// <summary>
        /// — Creates a validator ensuring the input matches a formatting pattern
        /// — once the required number of digits is present.
        /// — Registers the validator for field‑level execution.
        /// </summary>
        public Func<string?, Task<IEnumerable<string>>> Format(string fieldName, string pattern, int requiredLength)
        {
            var validator = new FormatRule(Messages, BlurTracker)
                .Create(fieldName, pattern, requiredLength);

            RegisterValidator(fieldName, v => validator((string?)v));
            return validator;
        }


        /// <summary>
        /// Combines multiple validators into a single sequential validator.
        /// Supports both string-based and object-based validators.
        /// Does not register anything for field‑level execution.
        /// </summary>
        public Func<object, Task<IEnumerable<string>>> ValidateAll(
            params Func<object, Task<IEnumerable<string>>>[] validators)
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
        /// Combines multiple string validators into a single sequential validator.
        /// Provided for backward compatibility with existing string-based validators.
        /// Does not register anything for field‑level execution.
        /// </summary>
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
        /// Creates a validator ensuring the model property contains exactly the specified
        /// number of digits. Validation evaluates the processed model value after input handlers
        /// have run. Triggers when the field has been blurred or when the required digit count
        /// has been reached.
        /// Registers the validator for field‑level execution.
        /// </summary>
        /// <param name="getModelValue">
        /// Function that retrieves the current value from the model property being validated.
        /// </param>
        /// <param name="fieldName">Field name used for tracking and message resolution.</param>
        /// <param name="exactDigits">Required number of digits.</param>
        /// <returns>Validator function that can be used in MudTextField Validation parameter.</returns>
        public Func<object, Task<IEnumerable<string>>> DigitsExactLength(
            Func<string?> getModelValue,
            string fieldName,
            int exactDigits)
        {
            var validator = new DigitsExactLengthRule(Messages, BlurTracker).Create(getModelValue, fieldName, exactDigits);
            RegisterValidator(fieldName, validator);
            return validator;
        }

        /// <summary>
        /// Creates a validator ensuring the input does not exceed the specified length.
        /// Registers the validator for field‑level execution.
        /// </summary>
        public Func<string?, Task<IEnumerable<string>>> MaxLength(string fieldName, int maxLength)
        {
            var validator = new MaxLengthRule(Messages).Create(fieldName, maxLength);
            RegisterValidator(fieldName, v => validator((string?)v));
            return validator;
        }

        /// <summary>
        /// Creates a validator ensuring the input meets the specified minimum length.
        /// Registers the validator for field‑level execution.
        /// </summary>
        public Func<string?, Task<IEnumerable<string>>> MinLength(string fieldName, int minLength)
        {
            var validator = new MinLengthRule(Messages).Create(fieldName, minLength);
            RegisterValidator(fieldName, v => validator((string?)v));
            return validator;
        }
        /// <summary>
        /// Triggers validation for a specific field using its registered validators.
        /// Intended for UI components that need to validate on blur or close.
        /// </summary>
        /// <param name="fieldName">Name of the field to validate.</param>
        /// <param name="value">Current value of the field.</param>
        public void ValidateField(string fieldName, object? value)
        {
            _ = ValidateFieldAsync(fieldName, value);
        }

    }
}
