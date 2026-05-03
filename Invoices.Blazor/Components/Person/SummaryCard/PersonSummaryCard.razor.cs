using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.SummaryCard
{
    /// <summary>
    /// Compact card displaying basic person information.
    /// Provides a link to the full person detail page.
    /// </summary>
    public partial class PersonSummaryCard : LocalizationComponentBase
    {
        /// <summary>Card title, e.g."Buyer" or "Seller".</summary>
        [Parameter] public string Title { get; set; } = string.Empty;

        /// <summary>The person to display.</summary>
        [Parameter] public PersonDto Person { get; set; } = default!;

        [Inject] private NavigationManager Nav { get; set; } = default!;
    }
}
