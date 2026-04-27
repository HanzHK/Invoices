using Microsoft.Extensions.DependencyInjection;

namespace Invoices.Blazor.Services
{
    /// <summary>
    /// Provides extension methods for registering application services
    /// into the dependency injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers UI-related services such as theme and layout management.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The same service collection for chaining.</returns>
        public static IServiceCollection AddUiServices(this IServiceCollection services)
        {
            services.AddScoped<DarkModeSwitchService>();
            return services;
        }

        /// <summary>
        /// Registers all API communication services used to interact
        /// with the backend REST API.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <returns>The same service collection for chaining.</returns>
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<PersonService>();
            services.AddScoped<InvoiceService>();
            return services;
        }
    }
}