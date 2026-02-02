using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.List
{
    public partial class PersonList
    {
        [Parameter] public IEnumerable<PersonDto>? Items { get; set; }
        [Parameter] public RenderFragment? Columns { get; set; }
    }

}
