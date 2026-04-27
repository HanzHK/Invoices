using Invoices.Blazor.Utils.Converters;
using Invoices.Shared.Models.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Invoices.Blazor.Configuration
{
    /// <summary>
    /// Extension methods for registering shared JsonSerializerOptions
    /// used across all HTTP services in the Blazor application.
    /// </summary>
    public static class JsonOptionsExtensions
    {
        /// <summary>
        /// Registers a singleton instance of JsonSerializerOptions
        /// configured with all required converters (Country enum, DateOnly, DateOnly?).
        /// </summary>
        public static IServiceCollection AddSharedJsonOptions(this IServiceCollection services)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Enum converter for PersonService
            options.Converters.Add(new UpperCaseEnumConverter<Country>());

            // Date converters for InvoiceService
            options.Converters.Add(new DateOnlyJsonConverter());
            options.Converters.Add(new NullableDateOnlyJsonConverter());

            services.AddSingleton(options);

            return services;
        }
    }
}
