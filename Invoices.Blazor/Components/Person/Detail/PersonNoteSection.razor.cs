using Invoices.Blazor.Components.Infrastructure.Sections;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.Detail
{
    public partial class PersonNoteSection : UnifiedSectionBase
    {
        [Parameter] public PersonDto Person { get; set; } = default!;
    }
}
