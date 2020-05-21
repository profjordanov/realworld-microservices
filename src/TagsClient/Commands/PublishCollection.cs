using MediatR;
using Optional;
using TagsService.Protos;
using YngStrs.Common;

namespace TagsClient.Commands
{
    public class PublishCollection : IRequest<Option<TagsView, Error>>
    {
        public PublishCollection(string[] tags, string articleId)
        {
            Tags = tags;
            ArticleId = articleId;
        }

        public string[] Tags { get; set; }

        public string ArticleId { get; set; }

        public PublishTags ToGrpcCmd()
        {
            return new PublishTags
            {
                ArticleId = ArticleId,
                Names = { Tags }
            };
        }
    }
}