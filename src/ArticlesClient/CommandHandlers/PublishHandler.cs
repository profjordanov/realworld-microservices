using System.Threading;
using System.Threading.Tasks;
using ArticlesClient.Commands;
using ArticlesClient.gRPCClient;
using Grpc.Core;
using MediatR;
using Optional;
using YngStrs.Common;

namespace ArticlesClient.CommandHandlers
{
    public class PublishHandler : IRequestHandler<Publish, Option<Unit, Error>>
    {
        private readonly IServiceClientFactory _clientFactory;

        public PublishHandler(IServiceClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Option<Unit, Error>> Handle(Publish command, CancellationToken cancellationToken)
        {
            try
            {
                var client = _clientFactory.Create();
                var result = await client.PublishAsync(command.ToPublishArticle());
                return Unit.Value.Some<Unit, Error>();
            }
            catch (RpcException)
            {
                return Option.None<Unit, Error>(
                    Error.Critical("An unhandled exception occured while persisting the article."));
            }
        }
    }
}