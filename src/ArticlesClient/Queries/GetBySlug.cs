using System.ComponentModel.DataAnnotations;
using ArticlesClient.Views;
using ArticlesService.Protos;
using MediatR;

namespace ArticlesClient.Queries
{
    public class GetBySlug : IRequest<ArticleProjection>
    {
        public GetBySlug(string slug)
        {
            Slug = slug;
        }

        [Required]
        public string Slug { get; set; }

        public BySlug ToGrpcQuery => new BySlug
        {
            Slug = Slug
        };
    }
}