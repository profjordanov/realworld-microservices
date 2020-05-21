using MediatR;
using Optional;
using TagsService.Protos;
using YngStrs.Common;

namespace TagsClient.Commands
{
    public class Publish : IRequest<Option<TagsView, Error>>
    {
        public string[] Tags { get; set; }

        public string ArticleId { get; set; }
    }
}