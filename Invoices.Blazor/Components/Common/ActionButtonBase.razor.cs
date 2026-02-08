using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Invoices.Blazor.Components.Common
{
    /// <summary>
    /// Base class for a reusable action button component.
    /// Provides a consistent API for icon, color, size, tooltip,
    /// and click handling while respecting the application's theme.
    /// </summary>
    public class ActionButtonBase : ComponentBase
    {
        /// <summary>
        /// Gets or sets the button color defined by the MudBlazor theme.
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// Gets or sets the visual variant of the button (Filled, Outlined, Text).
        /// </summary>
        [Parameter] public Variant Variant { get; set; } = Variant.Filled;

        /// <summary>
        /// Gets or sets the button size.
        /// </summary>
        [Parameter] public Size Size { get; set; } = Size.Medium;

        /// <summary>
        /// Gets or sets the icon displayed inside the button.
        /// If null or empty, no icon is rendered.
        /// </summary>
        [Parameter] public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the tooltip text shown when hovering over the button.
        /// If null or empty, the tooltip is disabled.
        /// </summary>
        [Parameter] public string? Tooltip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the button is disabled.
        /// </summary>
        [Parameter] public bool Disabled { get; set; }

        /// <summary>
        /// Callback invoked when the button is clicked.
        /// </summary>
        [Parameter] public EventCallback OnClick { get; set; }

        /// <summary>
        /// The content rendered inside the button (usually text).
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
