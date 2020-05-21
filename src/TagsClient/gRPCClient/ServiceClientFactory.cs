using System.Net.Http;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TagsClient.Settings;
using TagsService.Protos;

namespace TagsClient.gRPCClient
{
    public class ServiceClientFactory : IServiceClientFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly TagsServiceSettings _settings;

        public ServiceClientFactory(ILoggerFactory loggerFactory, IOptions<TagsServiceSettings> options)
        {
            _loggerFactory = loggerFactory;
            _settings = options.Value;
        }

        public TagService.TagServiceClient Create()
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
            var channel = GrpcChannel.ForAddress(_settings.TagsServerUrl, channelOptions);

            var client = new TagService.TagServiceClient(channel);

            return client;
        }
    }
}