using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderManager.Common.gRPCClients.ProductCatalog;

namespace RemoteProxy.Api.Configuration
{
    internal static class DependenciesConfiguration
    {
        public static IServiceCollection AddgRpcClients(
            this IServiceCollection collection,
            IConfiguration config)
        {
            var certificate = new X509Certificate2(
                config["Service:CertFileName"],
                config["Service:CertPassword"]);

            collection.AddSingleton<IProductCatalogClient>(
                provider => new ProductCatalogClient(
                    new LoggerFactory(),
                    config["Service:CatalogAddress"],
                    certificate));
            return collection;
        }
    }
}