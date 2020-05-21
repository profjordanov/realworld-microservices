using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TagsClient.gRPCClient;
using TagsClient.Settings;

namespace TagsClient.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTagsClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TagsServiceSettings>(configuration.GetSection("Services"));
            services.AddSingleton<IServiceClientFactory, ServiceClientFactory>();
            return services;
        }
    }
}