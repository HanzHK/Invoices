using Invoices.Blazor.Services.Localization;
using Invoices.Shared.Models.Common;

namespace Invoices.Blazor.Services.CountryAlias;

public class CountryAliasService : ICountryAliasService
{
    private readonly ILocalizationResolver _localizationResolver;

    private const string ResourceBaseName =
        "Invoices.Blazor.Services.CountryAlias.CountryAliasResources";

    public CountryAliasService(ILocalizationResolver localizationResolver)
    {
        _localizationResolver = localizationResolver;
    }

    public string GetAlias(Country country)
    {
        return _localizationResolver.Resolve(country.ToString(), ResourceBaseName);
    }
}