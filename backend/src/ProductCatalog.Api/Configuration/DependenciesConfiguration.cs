using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Api.Domain.Repositories;
using ProductCatalog.Api.Persistence;

namespace ProductCatalog.Api.Configuration
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IProductsRepository, ProductsRepository>();
            return services;
        }
    }
}