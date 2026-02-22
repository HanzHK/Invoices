using Invoices.Blazor.Components.Infrastructure.Sections;
using Invoices.Blazor.Services.CountryAlias;
using Invoices.Shared.Models.Common;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.Detail;

public partial class PersonAddressSection : UnifiedSectionBase
{
    [Inject] public ICountryAliasService CountryAliasService { get; set; } = default!;

    [Parameter] public PersonDto Person { get; set; } = default!;

    private string GetCountryAlias(Country country)
    {
        return CountryAliasService.GetAlias(country);
    }
}