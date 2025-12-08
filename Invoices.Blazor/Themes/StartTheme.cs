using MudBlazor;

namespace Invoices.Blazor.Themes;

public static class StartTheme
{
    public static MudTheme Theme = new MudTheme()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#827717",          
            Secondary = "#CDDC39",        
            Background = "#F9FBE7",
            Surface = "#FFFFFF",

            TextPrimary = "#212121",
            TextSecondary = "#424242",
            PrimaryContrastText = "#212121",

            AppbarBackground = "#CDDC39",
            AppbarText = "#212121",

            DrawerBackground = "#F0F4C3",
            DrawerText = "#212121",
            DrawerIcon = "#212121",

            Success = "#689F38",              
            Error = "#D32F2F",              
            Warning = "#F9A825",              
            Info = "#1976D2",           


            SuccessContrastText = "#212121",
            ErrorContrastText = "#212121",
            WarningContrastText = "#212121",
            InfoContrastText = "#212121"
        },

        PaletteDark = new PaletteDark()
        {
            Primary = "#C6FF00",          
            Secondary = "#AEEA00",        
            Background = "#212121",
            Surface = "#2E2E2E",

            TextPrimary = "#FFFFFF",
            TextSecondary = "#E0E0E0",
            PrimaryContrastText = "#000000",

            AppbarBackground = "#AFB42B",
            AppbarText = "#212121",

            DrawerBackground = "#424242",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#FFFFFF",

            Success = "#81C784",              
            Error = "#EF5350",              
            Warning = "#FFB74D",              
            Info = "#64B5F6",              

            SuccessContrastText = "#000000",
            ErrorContrastText = "#000000",
            WarningContrastText = "#000000",
            InfoContrastText = "#000000"

        },
        LayoutProperties = new LayoutProperties
        {
            AppbarHeight = "64px",          
            DrawerWidthLeft = "250px",      
            DefaultBorderRadius = "6px"     
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
