using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using ProductCatalog.Api;

namespace OrderManager.Common.gRPCClients.ProductCatalog
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly string _gRpcChannelAddress;
        private readonly X509Certificate2 _certificate;

        private ProductService.ProductServiceClient _client;

        public ProductCatalogClient(ILoggerFactory loggerFactory, string gRpcChannelAddress, X509Certificate2 certificate)
        {
            _loggerFactory = loggerFactory ??
                             throw new ArgumentNullException(nameof(loggerFactory));

            _gRpcChannelAddress = gRpcChannelAddress ??
                                  throw new ArgumentNullException(nameof(gRpcChannelAddress));

            _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
        }

        protected ProductService.ProductServiceClient Client
        {
            get
            {
                if (_client != null)
                {
                    return _client;
                }
                
                var handler = new HttpClientHandler();
                handler.ClientCertificates.Add(_certificate);

                var client = new HttpClient(handler);

                var channelOptions = new GrpcChannelOptions
                {
                    HttpClient = client,
                    LoggerFactory = _loggerFactory
                };

                var channel = GrpcChannel.ForAddress(_gRpcChannelAddress, channelOptions);

                _client = new ProductService.ProductServiceClient(channel);

                return _client;
            }
        }

        public async Task<ProductCatalogView> GetAllAsync()
        {
            var result = await Client.GetAllAsync(new Empty());
            return result;
        }

        public async Task<ProductView> GetByIdAsync(uint productId)
        {
            var result = await Client.GetByIdAsync(new GetCommand
            {
                ProductId = productId
            });

            return result;
        }
    }
}