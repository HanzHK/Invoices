using Invoices.Shared.Models.Common;

namespace Invoices.Blazor.Services.CountryAlias;

public interface ICountryAliasService
{
    string GetAlias(Country country);
}
