using ArticlesClient.gRPCClient;
using ArticlesClient.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesClient.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddArticlesClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ArticlesServiceSettings>(configuration.GetSection("Services"));
            services.AddSingleton<IServiceClientFactory, ServiceClientFactory>();
            return services;
        }
    }
}