using System.ComponentModel.DataAnnotations;
using ArticlesClient.Views;
using ArticlesService.Protos;
using MediatR;
using Optional;
using YngStrs.Common;

namespace ArticlesClient.Commands
{
    /// <summary>
    /// Publish new article command.
    /// </summary>
    public class Publish : IRequest<Option<ArticleProjection, Error>>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Body { get; set; }

        public string[] TagList { get; set; }

        public string AuthorId { get; set; }

        public PublishArticle ToPublishArticle()
        {
            return new PublishArticle
            {
                Title = Title,
                Body = Body,
                Description = Description,
                AuthorId = AuthorId
            };
        }
    }
}