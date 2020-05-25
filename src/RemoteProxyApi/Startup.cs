using ArticlesClient.CommandHandlers;
using ArticlesClient.DI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProfilesClient.Commands;
using ProfilesClient.Hubs;
using RemoteProxyApi.Configurations;
using TagsClient.Commands;
using TagsClient.DI;

namespace RemoteProxyApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTagsClient(Configuration);
            services.AddArticlesClient(Configuration);
            services.AddMediatR(
                typeof(Startup),
                typeof(PublishHandler),
                typeof(PublishCollection),
                typeof(FollowUser));
            services.AddControllers();
            services.AddOpenApi();
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //SignalR
                endpoints.MapHub<FollowUserHub>("/followUser");
            });
        }
    }
}
