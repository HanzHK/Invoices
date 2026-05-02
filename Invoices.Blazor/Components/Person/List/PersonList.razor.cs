using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.List;

public partial class PersonList
{
    [Parameter] public IEnumerable<PersonDto>? Items { get; set; }
    [Inject] public NavigationManager Nav { get; set; } = default!;

    private void HandleDelete(PersonDto person)
    {
        Items = Items?.Where(p => p.PersonId != person.PersonId).ToList();
        StateHasChanged();
    }
}