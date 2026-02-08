using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.List;

public partial class PersonList
{
    [Parameter] public IEnumerable<PersonDto>? Items { get; set; }
    [Parameter] public EventCallback<PersonDto> OnEdit { get; set; }
    [Parameter] public EventCallback<PersonDto> OnDelete { get; set; }
    [Parameter] public EventCallback<PersonDto> OnDetail { get; set; }

    private Task DeletePerson(PersonDto args)
    {
        throw new NotImplementedException();
    }
    private Task ViewPersonDetails(PersonDto args)
    {
        throw new NotImplementedException();
    }
    private Task EditPerson(PersonDto args)
    {
        throw new NotImplementedException();
    }
}