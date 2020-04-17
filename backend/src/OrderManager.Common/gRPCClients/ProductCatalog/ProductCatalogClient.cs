using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using ProductCatalog.Api;

namespace OrderManager.Common.gRPCClients.ProductCatalog
{
    public class ProductCatalogClient : IProductCatalogClient
    {
        private readonly ILoggerFactory _loggerFactory;

        private ProductService.ProductServiceClient _client;

        public ProductCatalogClient(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        protected ProductService.ProductServiceClient Client
        {
            get
            {
                if (_client != null)
                {
                    return _client;
                }

                var certificate = new X509Certificate2();

                var handler = new HttpClientHandler();
                handler.ClientCertificates.Add(certificate);

                var client = new HttpClient(handler);

                var opt = new GrpcChannelOptions
                {
                    HttpClient = client,
                    LoggerFactory = _loggerFactory
                };

                var channel = GrpcChannel.ForAddress("", opt);

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