using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Optional;
using YngStrs.Common;

namespace RemoteProxyApi.Controllers._Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        public ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
        protected IMediator Mediator { get; }

        protected Guid CurrentUserId => TryGetGuidClaim(ClaimTypes.NameIdentifier).ValueOr(Guid.Empty);

        protected IActionResult Error(Error error)
        {
            return error.Type switch
            {
                ErrorType.Validation => BadRequest(error),
                ErrorType.NotFound => NotFound(error),
                ErrorType.Unauthorized => Unauthorized(error),
                ErrorType.Conflict => Conflict(error),
                ErrorType.Critical => new ObjectResult(error)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                },
                _ => BadRequest(error),
            };
        }

        protected IActionResult NotFound(Error error) => NotFound((object)error);

        private Option<Guid> TryGetGuidClaim(string claimType)
        {
            var claimValue = User
                .Claims
                .FirstOrDefault(c => c.Type == claimType)?
                .Value;

            return claimValue
                .SomeNotNull()
                .Filter(v => Guid.TryParse(v, out _))
                .Map(v => new Guid(v));
        }
    }
}