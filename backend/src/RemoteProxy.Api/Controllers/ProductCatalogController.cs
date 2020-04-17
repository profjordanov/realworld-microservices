using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManager.Common.gRPCClients.ProductCatalog;

namespace RemoteProxy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCatalogController : ControllerBase
    {
        private readonly IProductCatalogClient _catalogClient;

        public ProductCatalogController(IProductCatalogClient catalogClient)
        {
            _catalogClient = catalogClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _catalogClient.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(uint productId)
        {
            var result = await _catalogClient.GetByIdAsync(productId);
            if (result == default)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}