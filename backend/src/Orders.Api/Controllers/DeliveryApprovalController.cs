using System.Threading.Tasks;
using Marten;
using Microsoft.AspNetCore.Mvc;
using Orders.Api.Domain.Entities;
using Orders.Api.Persistence;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryApprovalController : ControllerBase
    {
        private readonly IDocumentSession _session;

        public DeliveryApprovalController(IDocumentSession session)
        {
            _session = session;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var repo = new DeliveryApprovalRepository(_session);

            var order = new Order
            {
                Id = "1",
                City = "Varna"
            };

            await repo.ApproveByOrderAsync("USER A", order);
            await repo.RejectByOrderAsync("USER A", order);


            var result = await repo.GetBySomething();

            return Ok(result);
        }

        [HttpGet("approved")]
        public async Task<IActionResult> GetApproved()
        {
            var repo = new DeliveryApprovalRepository(_session);
            return Ok(await repo.GetApprovedDeliveries());
        }
    }
}