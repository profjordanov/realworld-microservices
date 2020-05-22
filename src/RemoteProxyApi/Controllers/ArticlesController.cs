using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesClient.Commands;
using ArticlesClient.Queries;
using ArticlesClient.Views;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RemoteProxyApi.Controllers._Base;
using YngStrs.Common;

namespace RemoteProxyApi.Controllers
{
    public class ArticlesController : ApiController
    {
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IMediator mediator, ILogger<ArticlesController> logger)
            : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of articles by various filters.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A list of articles.</returns>
        /// <response code="200">Successfully fetched the requested articles.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<ArticleProjection>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var result = await Mediator.Send(new AllArticles());
            return Ok(result);
        }

        /// <summary>
        /// Retrieves an article by its slug.
        /// </summary>
        /// <param name="slug">The slug.</param>
        /// <returns>An article or not found.</returns>
        /// <response code="200">An article.</response>
        /// <response code="404">Could not find the given article.</response>
        [HttpGet("{slug}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ArticleProjection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetBySlug(string slug)
        {
            var result = await Mediator.Send(new GetBySlug(slug));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Retrieves the logged in user's feed.
        /// </summary>
        /// <param name="limit">Limit of shown articles.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>A list of articles.</returns>
        /// <response code="200">The current users' feed.</response>
        /// <response code="404">If the id of the current user was not found (most likely a problem with the JWT claims).</response>
        [HttpGet("feed")]
        //[ProducesResponseType(typeof(ArticleModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFeed([FromQuery] int limit = 20, int offset = 0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Publishes an article.
        /// </summary>
        /// <param name="command">The article data.</param>
        /// <returns>An article model or an error.</returns>
        /// <response code="201">An article was successfully created and an article model was returned.</response>
        /// <response code="400">An error occurred.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ArticleProjection), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(Publish command)
        {
            return (await Mediator.Send(command)).Match(CreatedAtAction, Error);
        }

        /// <summary>
        /// Updates an article.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <param name="request">The article data.</param>
        /// <returns>An article model or an error.</returns>
        /// <response code="200">An article was successfully updated and an article model was returned.</response>
        /// <response code="400">An error occurred.</response>
        [HttpPut("{slug}")]
        //[ProducesResponseType(typeof(ArticleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(string slug)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an article.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <returns>The deleted article id or an error.</returns>
        /// <response code="204">Successfully deleted the article.</response>
        /// <response code="400">An error occurred.</response>
        [HttpDelete("{slug}")]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(string slug)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Favorites an article.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <returns>Ok or an error.</returns>
        /// <response code="200">Successfully favorited the given article.</response>
        [HttpPost("{slug}/favorite")]
        //[ProducesResponseType(typeof(ArticleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Favorite(string slug)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes an article favorite.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <returns>Ok or an error.</returns>
        /// <response code="200">Successfully unfavorited the given article.</response>
        [HttpDelete("{slug}/favorite")]
        //[ProducesResponseType(typeof(ArticleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Unfavorite(string slug)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all comments for a given article.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <returns>A list of comments.</returns>
        /// <response code="200">Got a list of comments for the given article.</response>
        [HttpGet("{slug}/comments")]
        //[ProducesResponseType(typeof(CommentModel[]), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetComments(string slug)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a comment.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <param name="request">The comment content.</param>
        /// <returns>The created comment.</returns>
        /// <response code="201">A comment was created.</response>
        [HttpPost("{slug}/comments")]
        //[ProducesResponseType(typeof(CommentModel[]), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComment(string slug)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="slug">The article slug.</param>
        /// <param name="commentId">The comment id.</param>
        /// <returns>No content result.</returns>
        /// <response code="204">The comment was successfully deleted.</response>
        [HttpDelete("{slug}/comments/{commentId:int}")]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteComment(string slug, int commentId)
        {
            throw new NotImplementedException();
        }

        private IActionResult CreatedAtAction(ArticleProjection projection)
        {
            return CreatedAtAction(
                nameof(GetBySlug),
                new { slug  = projection.Slug },
                projection);
        }
    }
}