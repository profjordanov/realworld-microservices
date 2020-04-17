using System.Collections.Generic;
using ProductCatalog.Api.Domain.Entities;

namespace ProductCatalog.Api.Domain.Repositories
{
    public interface IProductsRepository
    {
        IEnumerable<Product> All { get; }

        Product GetByIdOrDefault(ulong productId);

        Product AddOrDefault(Product product);

        Product UpdateOrDefault(Product product);
    }
}