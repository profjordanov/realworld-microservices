using System;
using MediatR;
using TagsService.Protos;

namespace TagsClient.Queries
{
    public class AllByArticleId : IRequest<TagsView>
    {
        public AllByArticleId(string articleId)
        {
            ArticleId = articleId;
        }

        public AllByArticleId(Guid articleId)
        {
            ArticleId = articleId.ToString();
        }

        public string ArticleId { get; set; }

        public ByArticleId ToGrpcQuery => new ByArticleId
        {
            ArticleId = ArticleId
        };
    }
}