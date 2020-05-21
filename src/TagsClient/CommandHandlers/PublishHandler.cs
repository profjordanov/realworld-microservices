using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Optional;
using TagsClient.Commands;
using TagsService.Protos;
using YngStrs.Common;

namespace TagsClient.CommandHandlers
{
    public class PublishHandler : IRequestHandler<PublishCollection, Option<TagsView, Error>>
    {
        public Task<Option<TagsView, Error>> Handle(PublishCollection command, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}