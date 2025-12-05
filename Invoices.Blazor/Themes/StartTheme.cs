using MudBlazor;

namespace Invoices.Blazor.Themes;

public static class StartTheme
{
    public static MudTheme Theme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#1976d2",
            Secondary = "#ff4081",
            Background = "#f5f5f5",
            Surface = "#ffffff",
            AppbarBackground = "#1976d2",
            DrawerBackground = "#eeeeee",
            Success = "#4caf50",
            Error = "#f44336",
            Warning = "#ff9800",
            Info = "#2196f3"
        },
        PaletteDark = new PaletteDark()
        {
            Primary = "#90caf9",
            Secondary = "#f48fb1",
            Background = "#121212",
            Surface = "#1e1e1e",
            AppbarBackground = "#333333",
            DrawerBackground = "#252525",
            Success = "#81c784",
            Error = "#e57373",
            Warning = "#ffb74d",
            Info = "#64b5f6"
        },
        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Poppins", "Helvetica", "Arial", "sans-serif" },
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.43",
                LetterSpacing = ".01071em"
            },
            H1 = new H1Typography()
            {
                FontSize = "6rem",
                FontWeight = "300",
                LineHeight = "1.167",
                LetterSpacing = "-.01562em"
            },
            Button = new ButtonTypography()
            {
                FontSize = "0.875rem",
                FontWeight = "500",
                LineHeight = "1.75",
                LetterSpacing = ".02857em",
                TextTransform = "uppercase"
            }
        }
    };
}
