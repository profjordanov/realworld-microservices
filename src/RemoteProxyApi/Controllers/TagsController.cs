using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemoteProxyApi.Controllers._Base;
using TagsClient.Queries;

namespace RemoteProxyApi.Controllers
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
            var result = await Mediator.Send(new AllTags());
            var tagNames = result.Tags?.Select(view => view.Name);
            return Ok(tagNames);
        }
    }
}