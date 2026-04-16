using Invoices.Blazor.Components.Infrastructure.Localization;

namespace Invoices.Blazor.Layout.Appbar
{

    public partial class LangMenu : LocalizationComponentBase
    {
        private async Task ChangeLanguage(string culture)
        {
            await Language.SetCultureAsync(culture);
        }
    }
}
