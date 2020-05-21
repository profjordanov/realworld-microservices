using Microsoft.AspNetCore.Builder;

namespace RemoteProxyApi.Configurations
{
    public static class MiddlewareConfigurations
    {
        public static void UseOpenApi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.RoutePrefix = "swagger";

                setup.SwaggerEndpoint(
                    url: "/swagger/RemoteProxyOpenAPISpecifications/swagger.json",
                    name: "RemoteProxy");
            });
        }
    }
}