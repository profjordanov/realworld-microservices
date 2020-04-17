using Microsoft.Extensions.DependencyInjection;
using OrderManager.Common.gRPCClients.ProductCatalog;

namespace RemoteProxy.Api.Configuration
{
    internal static class DependenciesConfiguration
    {
        public static IServiceCollection AddgRpcClients(this IServiceCollection collection)
        {
            collection.AddSingleton<IProductCatalogClient, ProductCatalogClient>();
            return collection;
        }
    }
}