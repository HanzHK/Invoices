using MudBlazor;

namespace Invoices.Blazor.Client.Theme
{
    public static class AppTheme
    {
        public static MudTheme Light = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = Colors.Blue.Darken2,
                Secondary = Colors.Orange.Accent3,
                Background = Colors.Gray.Lighten5,
                DrawerBackground = Colors.Gray.Lighten3,
                Surface = Colors.Shades.White,
            },
            Typography = new Typography()
            {
                H6 = new DefaultTypography()
                {
                    FontSize = "1.2rem",
                    FontWeight = "600"
                },
                Body1 = new DefaultTypography()
                {
                    FontSize = "0.95rem"
                }
            }
        };

        public static MudTheme Dark = new MudTheme()
        {
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Blue.Lighten3,
                Secondary = Colors.Orange.Accent2,
                Background = Colors.Gray.Darken4,
                DrawerBackground = Colors.Gray.Darken3,
                Surface = Colors.Gray.Darken4,
            }
        };
    }
}
