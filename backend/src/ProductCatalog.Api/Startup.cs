using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCatalog.Api.Configuration;

namespace ProductCatalog.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication()
                .AddCertificate(opt =>
                {
                    opt.AllowedCertificateTypes = CertificateTypes.SelfSigned;
                    opt.RevocationMode = X509RevocationMode.NoCheck; // Self-Signed Certs (Development)
                    opt.Events = new CertificateAuthenticationEvents()
                    {
                        OnCertificateValidated = ctx =>
                        {
                            // Write additional Validation  
                            ctx.Success();
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();


            services.AddRepositories();
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductsService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
