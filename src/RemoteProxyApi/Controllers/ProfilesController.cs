using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RemoteProxyApi.Controllers._Base;
using YngStrs.Common;

namespace RemoteProxyApi.Controllers
{
    public class ProfilesController : ApiController
    {
        public ProfilesController(IMediator mediator)
            : base(mediator)
        {
        }

        /// <summary>
        /// Retrieves a user's profile by username.
        /// </summary>
        /// <param name="username">The username to look for.</param>
        /// <returns>A user profile or not found.</returns>
        /// <response code="200">Returns the user's profile.</response>
        /// <response code="404">No user with tha given username was found.</response>
        [HttpGet("{username}")]
        //[ProducesResponseType(typeof(UserProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Follows a user.
        /// </summary>
        /// <param name="username">The user to follow.</param>
        /// <returns>The followed user's profile or an error.</returns>
        /// <response code="200">Successfully followed the given user.</response>
        /// <response code="400">An eror occurred.</response>
        [HttpPost("{username}/follow")]
        //[ProducesResponseType(typeof(UserProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Follow(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unfollows a user.
        /// </summary>
        /// <param name="username">The user to unfollow.</param>
        /// <returns>The unfollowed user's profile or an error.</returns>
        /// <response code="200">Successfully unfollowed the given user.</response>
        /// <response code="400">An eror occurred.</response>
        [HttpDelete("{username}/follow")]
        //[ProducesResponseType(typeof(UserProfileModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Unfollow(string username)
        {
            throw new NotImplementedException();
        }
    }
}