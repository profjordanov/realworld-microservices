using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemoteProxy.Api.Controllers._Base;

namespace RemoteProxy.Api.Controllers
{
    public class TagsController : ApiController
    {
        public TagsController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Gets all tags.
        /// </summary>
        /// <returns>A list of tags.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }
    }
}