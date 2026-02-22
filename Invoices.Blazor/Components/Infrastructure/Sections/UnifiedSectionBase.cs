using Microsoft.AspNetCore.Components;
using Invoices.Blazor.Components.Infrastructure.Localization;

namespace Invoices.Blazor.Components.Infrastructure.Sections;

public abstract class UnifiedSectionBase : LocalizationComponentBase
{
    [CascadingParameter] public int Columns { get; set; } = 1;

    protected int ColumnSize => 12 / Columns;
}
