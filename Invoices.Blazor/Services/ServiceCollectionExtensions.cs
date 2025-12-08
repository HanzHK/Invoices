using Microsoft.Extensions.DependencyInjection;

namespace Invoices.Blazor.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services)
        {

            services.AddScoped<DarkModeSwitchService>();
            return services;
        }
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<PersonService>();
       


            return services;
        }
    }
}
