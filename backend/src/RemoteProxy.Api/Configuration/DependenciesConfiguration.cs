using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderManager.Common.gRPCClients.ProductCatalog;

namespace RemoteProxy.Api.Configuration
{
    internal static class DependenciesConfiguration
    {
        public static IServiceCollection AddgRpcClients(this IServiceCollection collection)
        {
            collection.AddSingleton<IProductCatalogClient>(
                provider => new ProductCatalogClient(loggerFactory:new LoggerFactory(), "https://localhost:32011"));
            return collection;
        }
    }
}