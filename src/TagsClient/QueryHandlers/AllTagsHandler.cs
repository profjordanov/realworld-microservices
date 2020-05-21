using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using TagsClient.gRPCClient;
using TagsClient.Queries;
using TagsService.Protos;

namespace TagsClient.QueryHandlers
{
    public class AllTagsHandler : IRequestHandler<AllTags, TagsView>
    {
        private readonly IServiceClientFactory _clientFactory;

        public AllTagsHandler(IServiceClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TagsView> Handle(AllTags request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.Create();
            return await client.GetAllAsync(new Empty());
        }
    }
}