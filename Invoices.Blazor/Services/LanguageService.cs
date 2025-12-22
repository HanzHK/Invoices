using System.Globalization;
namespace Invoices.Blazor.Services
{
    public class LanguageService
    {
        public event Action? OnChange;

        public void SetLanguage(string culture)
        {
            var ci = new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

            OnChange?.Invoke();
        }
    }

}
