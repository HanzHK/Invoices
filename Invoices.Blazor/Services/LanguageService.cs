using System.Globalization;
using Microsoft.JSInterop;

namespace Invoices.Blazor.Services
{
    /// <summary>
    /// Manages application language, culture settings, and persistence in browser storage.
    /// </summary>
    public class LanguageService
    {
        private readonly IJSRuntime _js;
        private const string StorageKey = "app_language";

        /// <summary>
        /// Fired when the application language changes.
        /// Components can subscribe to re-render on language switch.
        /// </summary>
        public event Action? OnChange;

        /// <summary>
        /// Currently active culture code (e.g., "en-US", "cs-CZ").
        /// </summary>
        public string CurrentCulture { get; private set; } = "cs-CZ";

        /// <summary>
        /// Creates a new instance of LanguageService with JS interop support.
        /// </summary>
        public LanguageService(IJSRuntime js)
        {
            _js = js;
        }

        /// <summary>
        /// Loads the saved language from localStorage and applies it.
        /// Called once during application startup.
        /// </summary>
        public async Task InitializeAsync()
        {
            var stored = await _js.InvokeAsync<string>("localStorage.getItem", StorageKey);

            if (!string.IsNullOrWhiteSpace(stored))
            {
                // Apply culture without triggering UI refresh
                ApplyCulture(stored);
                CurrentCulture = stored;
            }
            else
            {
                // Persist default language if nothing is stored
                await SaveToStorage(CurrentCulture);
            }
        }

        /// <summary>
        /// Changes the application language, updates culture settings,
        /// persists the value, and notifies subscribers.
        /// </summary>
        public async Task SetLanguage(string culture)
        {
            ApplyCulture(culture);

            CurrentCulture = culture;

            await SaveToStorage(culture);

            OnChange?.Invoke();
        }

        /// <summary>
        /// Applies the given culture to the current thread.
        /// </summary>
        private void ApplyCulture(string culture)
        {
            var ci = new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
        }

        /// <summary>
        /// Saves the selected language to browser localStorage.
        /// </summary>
        private Task SaveToStorage(string culture)
        {
            return _js.InvokeVoidAsync("localStorage.setItem", StorageKey, culture).AsTask();
        }

        /// <summary>
        /// Changes the current culture, persists it to localStorage,
        /// applies it to .NET, and notifies subscribers.
        /// </summary>
        public async Task SetCultureAsync(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture))
                return;

            CurrentCulture = culture;

            ApplyCulture(culture);
            await SaveToStorage(culture);

            OnChange?.Invoke();


        }
    }
}
