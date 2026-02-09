using Invoices.Shared.Models.Person;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.PersonActionButtons
{
    public partial class PersonActionButtonsBase : ComponentBase
    {
        [Parameter] public PersonDto PersonModel { get; set; } = default!;

        [Parameter] public EventCallback<PersonDto> OnEdit { get; set; }
        [Parameter] public EventCallback<PersonDto> OnDelete { get; set; }
        [Parameter] public EventCallback<PersonDto> OnDetail { get; set; }
    }
}
