using Invoices.Shared.Models.Common;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using static MudBlazor.CategoryTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Invoices.Blazor.Components.Validators
{
    public class PersonFormValidator
    {
        private readonly IStringLocalizer<PersonFormValidator> L;

        public PersonFormValidator(IStringLocalizer<PersonFormValidator> localizer)
        {
            L = localizer;
        }

        private string? ValidateRequired(string value, string fieldKey)
        {
            if (string.IsNullOrWhiteSpace(value))
                return L[$"{fieldKey}Required"];

            return null;
        }
        public Func<string, string?> Required(string fieldKey)
        {
            return value => ValidateRequired(value, fieldKey);
        }
        /// <summary>
        /// Creates a validation function that checks whether the input string has the specified length.
        /// </summary>
        /// <param name="fieldKey">The key that identifies the field being validated. Used to generate error messages if validation fails.</param>
        /// <param name="length">The required length that the input string must have. Must be non-negative.</param>
        /// <returns>A function that takes a string value and returns null if the value is null, empty, or has the specified
        /// length; otherwise, returns a localized error message.</returns>
        public Func<string, string?> Length(string fieldKey, int length)
        {
            return value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return null; 
                if (value.Length != length)
                    return L[$"{fieldKey}Length", length];

                return null;
            };
        }
        /// <summary>
        /// Creates a validation function that checks whether the input string matches a specified format pattern.
        /// </summary>
        /// <param name="fieldKey">The key that identifies the field being validated. Used to generate localized error messages if validation fails.  </param>
        /// <param name="pattern">The regular expression pattern that defines the required format of the input string.  </param>
        /// <returns>A function that takes a string value and returns null if the value is null, empty, or matches the specified pattern;
        /// otherwise, returns a localized error message indicating an invalid format.
        /// </returns>
        public Func<string, string?> Format(string fieldKey, string pattern)
        {
            return value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return null;

                if (!System.Text.RegularExpressions.Regex.IsMatch(value, pattern))
                    return L[$"{fieldKey}Format"];

                return null;
            };
        }


        /// <summary>
        /// Creates a composite validation function that executes multiple validation rules in sequence. (required by MudBlazor)
        /// </summary>
        /// <param name="validators">One or more validation functions that each accept a string value and return either null (if valid) or a localized error message.</param>
        /// <returns>A single validation function that runs all provided validators in order and returns the first error encountered;
        /// returns null if all validation rules pass successfully.
        /// </returns>

        public Func<string, string?> ValidateAll(params Func<string, string?>[] validators)
        {
            return value =>
            {
                foreach (var validator in validators)
                {
                    var result = validator(value);
                    if (result is not null)
                        return result; 
                }

                return null; 
            };
        }


        public Func<Country?, string?> RequiredCountry(string fieldKey)
        {
            return value =>
            {
                if (value is null)
                    return L[$"{fieldKey}Required"];

                return null;
            };
        }





    }

}
