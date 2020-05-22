using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TagsClient.gRPCClient;
using TagsClient.Queries;
using TagsService.Protos;

namespace TagsClient.QueryHandlers
{
    public class AllByArticleIdHandler : IRequestHandler<AllByArticleId, TagsView>
    {
        private readonly IServiceClientFactory _clientFactory;

        public AllByArticleIdHandler(IServiceClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<TagsView> Handle(AllByArticleId request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.Create();
            return await client.GetByArticleAsync(request.ToGrpcQuery);
        }
    }
}