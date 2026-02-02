using Invoices.Blazor.Components.Infrastructure.Localization;
using Invoices.Blazor.Services.Localization;
using Microsoft.AspNetCore.Components;

namespace Invoices.Blazor.Components.Person.List.Columns
{
    /// <summary>
    /// Column component displaying the person's name with a localized title.
    /// </summary>
    public class PersonNameColumnBase : BaseColumn
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonNameColumnBase"/> class.
        /// </summary>
        public PersonNameColumnBase()
        {
        }

        /// <summary>
        /// Localized column title.
        /// </summary>
        protected string Title => T("Name");
    }
}
