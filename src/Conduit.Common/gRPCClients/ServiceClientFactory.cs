using System;
using System.Net.Http;
using Conduit.Common.Settings;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace Conduit.Common.gRPCClients
{
    [Obsolete]
    public class ServiceClientFactory<T>
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ServicesSettings _settings;

        public ServiceClientFactory(ILoggerFactory loggerFactory, ServicesSettings settings)
        {
            _loggerFactory = loggerFactory;
            _settings = settings;
        }

        public void Create<T>()
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
        }

    }
}