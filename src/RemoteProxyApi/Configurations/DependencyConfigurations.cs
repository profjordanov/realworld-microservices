using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RemoteProxyApi.Configurations
{
    public static class DependencyConfigurations
    {
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "RemoteProxyOpenAPISpecifications",
                    new OpenApiInfo
                    {
                        Title = "Conduit Api Gateway",
                        Version = "1"
                    });
            });
            return services;
        }
    }
}