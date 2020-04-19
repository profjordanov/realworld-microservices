using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Api.Domain.Repositories;
using ProductCatalog.Api.Persistence;

namespace ProductCatalog.Api.Configuration
{
    internal static class DependenciesConfiguration
    {
        internal static IServiceCollection AddCertificateAuthentication(this IServiceCollection services)
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
                        },
                        OnAuthenticationFailed = ctx =>
                        {
                            ctx.Fail("Certificate failed!");
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            return services;
        }
        internal static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IProductsRepository, ProductsRepository>();
            return services;
        }
    }
}