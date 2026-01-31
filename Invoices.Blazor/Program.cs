using Invoices.Blazor.Services;
using Invoices.Blazor.Validation;
using Invoices.Blazor.Validation.Specific;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Localization;
using MudBlazor.Services;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Invoices.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient with custom JsonSerializerOptions
            builder.Services.AddScoped(sp =>
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new JsonStringEnumConverter(namingPolicy: null, allowIntegerValues: false));

                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://invoices-e9beeabjdgf5afcr.polandcentral-01.azurewebsites.net")
                };

                // attach options to client via extension
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            });

            // Register LanguageService
            builder.Services.AddScoped<LanguageService>();

            // Register FormFieldBlurTracker
            builder.Services.AddScoped<FormFieldBlurTracker>();

            // Add localization services
            builder.Services.AddLocalization();

            // Set default culture to Czech
            var defaultCulture = new CultureInfo("cs"); 
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            // Register Validators
            builder.Services.AddScoped<FormValidator>(sp =>
            {
                var factory = sp.GetRequiredService<IStringLocalizerFactory>();
                var blur = sp.GetRequiredService<FormFieldBlurTracker>();


                throw new InvalidOperationException("Component must supply its type");
            });

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Register extension services
            builder.Services.AddUiServices();

            // Register API services
            builder.Services.AddApiServices();

            await builder.Build().RunAsync();
        }
    }
}
