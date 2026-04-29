using Invoices.Blazor.Configuration;
using Invoices.Blazor.Infrastructure;
using Invoices.Blazor.Services;
using Invoices.Blazor.Services.CountryAlias;
using Invoices.Blazor.Services.Localization;
using Invoices.Blazor.Services.UI.Actions;
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
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://invoices-e9beeabjdgf5afcr.polandcentral-01.azurewebsites.net")
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            });

            // Register LanguageService
            builder.Services.AddSingleton<LanguageService>();

            // Register FormFieldBlurTracker
            builder.Services.AddScoped<FormFieldBlurTracker>();

            // Register PersonActions
            builder.Services.AddScoped<IPersonActions, PersonActions>();

            // Add localization services
            builder.Services.AddLocalization();

            // Register Validators
            builder.Services.AddScoped<FormValidator>(sp =>
            {
                var factory = sp.GetRequiredService<IStringLocalizerFactory>();
                var blur = sp.GetRequiredService<FormFieldBlurTracker>();


                throw new InvalidOperationException("Component must supply its type");
            });

            // Register LocalizationResolver
            builder.Services.AddSingleton<ILocalizationResolver, LocalizationResolver>();

            // Register CountryAliasService
            builder.Services.AddScoped<ICountryAliasService, CountryAliasService>();

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Register extension services
            builder.Services.AddUiServices();

            // Register API services
            builder.Services.AddApiServices();

            // Register shared JsonSerializerOptions (Country + DateOnly + DateOnly?)
            builder.Services.AddSharedJsonOptions();

            // Register ApiResultHandler (required for PersonService + InvoiceService)
            builder.Services.AddScoped<ApiResultHandler>();

            var host = builder.Build();

            // Initialize language from localStorage
            var lang = host.Services.GetRequiredService<LanguageService>();
            await lang.InitializeAsync();

            await host.RunAsync();

        }
    }
}
