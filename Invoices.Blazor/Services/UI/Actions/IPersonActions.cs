using Invoices.Shared.Models.Person;
using System.Threading.Tasks;

namespace Invoices.Blazor.Services.UI.Actions
{
    /// <summary>
    /// Defines a set of high-level UI actions related to person entities.
    /// These actions encapsulate navigation and domain service calls.
    /// 
    /// Localization and user notifications are intentionally NOT handled here.
    /// UI components (such as PersonActionButtons) are responsible for
    /// displaying localized messages and snackbars.
    /// </summary>
    public interface IPersonActions
    {
        /// <summary>
        /// Navigates to the edit page for the specified person.
        /// </summary>
        /// <param name="person">The person to edit.</param>
        Task Edit(PersonDto person);

        /// <summary>
        /// Deletes the specified person using the domain service.
        /// UI components are responsible for showing notifications.
        /// </summary>
        /// <param name="person">The person to delete.</param>
        Task Delete(PersonDto person);

        /// <summary>
        /// Navigates to the detail page for the specified person.
        /// </summary>
        /// <param name="person">The person whose details should be displayed.</param>
        Task View(PersonDto person);
    }
}
