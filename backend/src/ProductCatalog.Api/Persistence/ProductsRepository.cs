using System.Collections.Concurrent;
using System.Collections.Generic;
using ProductCatalog.Api.Domain.Entities;
using ProductCatalog.Api.Domain.Repositories;

namespace ProductCatalog.Api.Persistence
{
    public class ProductsRepository : IProductsRepository
    {
        private static readonly List<KeyValuePair<ulong, Product>> KeyValuePairs = new List<KeyValuePair<ulong, Product>>
        {
            new KeyValuePair<ulong, Product>(1, new Product
            {
                Id = 1,
                Name = "Product 1",
                Quantity = 10,
                Description = "Product 1",
                Price = 150.99m,
                Base64Image = string.Empty
            }),
            new KeyValuePair<ulong, Product>(2, new Product
            {
                Id = 2,
                Name = "Product 2",
                Quantity = 10,
                Description = "Product 2",
                Price = 90.99m,
                Base64Image = string.Empty
            }),
            new KeyValuePair<ulong, Product>(3, new Product
            {
                Id = 3,
                Name = "Product 3",
                Quantity = 10,
                Description = "Product 3",
                Price = 10.99m,
                Base64Image = string.Empty
            })
        };

        private readonly ConcurrentDictionary<ulong, Product> _dictionary =
            new ConcurrentDictionary<ulong, Product>(KeyValuePairs);

        public IEnumerable<Product> All => _dictionary.Values;

        public Product GetByIdOrDefault(ulong productId)
        {
            var hasValue = _dictionary.TryGetValue(productId, out var product);
            return hasValue ? product : default;
        }

        public Product AddOrDefault(Product product)
        {
            var added = _dictionary.TryAdd(product.Id, product);
            return added ? product : default;
        }

        public Product UpdateOrDefault(Product product)
        {
            var hasValue = _dictionary.TryGetValue(product.Id, out _);

            if (!hasValue)
            {
                return default;
            }

            _dictionary[product.Id] = product;
            return product;
        }
    }

}