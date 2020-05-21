using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using ArticlesClient.Settings;
using ArticlesService.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ArticlesClient.gRPCClient
{
    public class ServiceClientFactory : IServiceClientFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ArticlesServiceSettings _settings;

        public ServiceClientFactory(ILoggerFactory loggerFactory, IOptions<ArticlesServiceSettings> options)
        {
            _loggerFactory = loggerFactory;
            _settings = options.Value;
        }

        public ArticleService.ArticleServiceClient Create()
        {
            //var certificate = new X509Certificate2(_settings.CertFileName, _settings.CertPassword);
            var handler = new HttpClientHandler();
            //handler.ClientCertificates.Add(certificate);

            var httpClient = new HttpClient(handler);
            var channelOptions = new GrpcChannelOptions
            {
                HttpClient = httpClient,
                LoggerFactory = _loggerFactory
            };
            var channel = GrpcChannel.ForAddress(_settings.ArticlesServerUrl, channelOptions);

            var client = new ArticleService.ArticleServiceClient(channel);

            return client;
        }
    }
}