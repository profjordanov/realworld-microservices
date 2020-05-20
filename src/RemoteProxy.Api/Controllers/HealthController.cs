using Microsoft.AspNetCore.Mvc;
using RemoteProxy.Api.Controllers._Base;

namespace RemoteProxy.Api.Controllers
{
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Health() => Ok();
    }
}