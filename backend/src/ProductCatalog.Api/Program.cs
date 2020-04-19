using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProductCatalog.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options =>
                    {
                        ConfigureServerCertificates(options);
                    });
                });

        private static void ConfigureServerCertificates(KestrelServerOptions options)
        {
            var config = options.ApplicationServices.GetService<IConfiguration>();
            var cert = new X509Certificate2(
                config["Certificate:File"],
                config["Certificate:Password"]);

            options.ConfigureHttpsDefaults(adapterOptions =>
            {
                adapterOptions.ClientCertificateMode = ClientCertificateMode.AllowCertificate;
                adapterOptions.CheckCertificateRevocation = false;
                adapterOptions.ServerCertificate = cert;
            });
        }
    }
}
