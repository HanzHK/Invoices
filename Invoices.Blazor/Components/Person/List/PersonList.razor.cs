using Invoices.Blazor.Services;
using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;
using System;

namespace Invoices.Blazor.Components.Person.List;

public partial class PersonList
{
    [Parameter] public IEnumerable<PersonDto>? Items { get; set; }
    [Parameter] public EventCallback<PersonDto> OnEdit { get; set; }
    [Parameter] public EventCallback<PersonDto> OnDelete { get; set; }
    [Parameter] public EventCallback<PersonDto> OnDetail { get; set; }

    private Task DeletePerson(PersonDto person)
    {
        return OnDelete.InvokeAsync(person);
    }

    private Task ViewPersonDetails(PersonDto person)
    {
        return OnDetail.InvokeAsync(person);
    }
    private Task EditPerson(PersonDto person)
    {
        return OnEdit.InvokeAsync(person);
    }
}