using Invoices.Shared.Models.Common;
using Microsoft.Extensions.Localization;

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
