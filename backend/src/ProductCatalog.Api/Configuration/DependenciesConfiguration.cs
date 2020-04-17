using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Api.Domain.Repositories;
using ProductCatalog.Api.Persistence;

namespace ProductCatalog.Api.Configuration
{
    internal static class DependenciesConfiguration
    {
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IProductsRepository, ProductsRepository>();
            return services;
        }
    }
}