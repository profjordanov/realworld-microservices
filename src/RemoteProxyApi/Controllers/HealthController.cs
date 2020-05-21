using Microsoft.AspNetCore.Mvc;

namespace RemoteProxyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Health() => Ok("Up and running!");
    }
}