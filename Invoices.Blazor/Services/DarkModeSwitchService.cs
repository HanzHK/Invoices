namespace Invoices.Blazor.Services
{
    public class DarkModeSwitchService
    {
        public bool DrawerOpen { get; set; } = true;  
        public bool IsDarkMode { get; set; } = true; 
        public event Action? OnChange;

        public void ToggleDrawer()
        {
            DrawerOpen = !DrawerOpen;
            NotifyStateChanged();
        }

        public void ToggleTheme()
        {
            IsDarkMode = !IsDarkMode;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}