using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.Detail
{
    public partial class PersonDetail
    {
        [Parameter] public PersonDto Person { get; set; } = default!;
    }
}
