using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using ProductCatalog.Api.Domain.Repositories;

namespace ProductCatalog.Api
{
    [Authorize(AuthenticationSchemes = CertificateAuthenticationDefaults.AuthenticationScheme)]
    public class ProductsService : ProductService.ProductServiceBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public override Task<ProductCatalogView> GetAll(Empty request, ServerCallContext context)
        {
            var result = _productsRepository
                .All
                .Select(product => product.ToView());

            return Task.FromResult(new ProductCatalogView
            {
                Products = { result }
            });
        }

        public override Task<ProductView> GetById(GetCommand request, ServerCallContext context)
        {
            var result = _productsRepository.GetByIdOrDefault(request.ProductId);

            return Task.FromResult(result?.ToView());
        }

    }
}