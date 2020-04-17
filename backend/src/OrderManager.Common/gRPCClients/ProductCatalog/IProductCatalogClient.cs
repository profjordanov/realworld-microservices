using System.Threading.Tasks;
using ProductCatalog.Api;

namespace OrderManager.Common.gRPCClients.ProductCatalog
{
    public interface IProductCatalogClient
    {
        Task<ProductCatalogView> GetAllAsync();

        Task<ProductView> GetByIdAsync(uint productId);
    }
}