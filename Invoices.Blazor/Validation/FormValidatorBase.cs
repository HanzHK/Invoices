using Invoices.Blazor.Validation.Infrastructure;
using Microsoft.Extensions.Localization;

namespace Invoices.Blazor.Validation
{
    /// <summary>
    /// Provides shared infrastructure for form validators, including access to
    /// localized resources and blur tracking. Concrete validators supply the
    /// primary localizer (typically bound to the hosting component) and an
    /// optional fallback localizer (e.g. for generic validation messages).
    /// </summary>
    public abstract class FormValidatorBase
    {
        /// <summary>
        /// Primary localizer used for resolving validation messages that are
        /// specific to the current form or component (e.g. PersonForm.resx).
        /// </summary>
        protected readonly IStringLocalizer Primary;

        /// <summary>
        /// Fallback localizer used for resolving generic validation messages
        /// shared across forms (e.g. FormValidator.resx).
        /// </summary>
        protected readonly IStringLocalizer Fallback;

        /// <summary>
        /// Service responsible for tracking blur state of individual form fields,
        /// enabling validation rules that should only execute after the user
        /// leaves a field.
        /// </summary>
        protected readonly FormFieldBlurTracker BlurTracker;

        /// <summary>
        /// Provides centralized resolution of localized validation messages,
        /// supporting both component‑specific resource keys and generic fallback
        /// keys shared across all validators.
        /// </summary>
        private protected readonly MessageResolver Messages;


        /// <summary>
        /// Initializes a new instance of the <see cref="FormValidatorBase"/> class,
        /// setting up component‑specific and fallback localizers, blur tracking,
        /// and internal message resolution infrastructure.
        /// </summary>
        /// <param name="primary">
        /// Primary localizer bound to the hosting component, used for
        /// component‑specific validation messages.
        /// </param>
        /// <param name="fallback">
        /// Fallback localizer used for generic validation messages when a
        /// component‑specific resource key is not found.
        /// </param>
        /// <param name="blurTracker">
        /// Service used to track blur state of form fields.
        /// </param>
        protected FormValidatorBase(
            IStringLocalizer primary,
            IStringLocalizer fallback,
            FormFieldBlurTracker blurTracker)
        {
            Primary = primary;
            Fallback = fallback;
            BlurTracker = blurTracker;
            Messages = new MessageResolver(primary, fallback);
        }

        /// <summary>
        /// Convenience accessor for the primary localizer, used by existing
        /// validation methods that do not need explicit fallback handling.
        /// </summary>
        protected IStringLocalizer Localizer => Primary;
    }
}
