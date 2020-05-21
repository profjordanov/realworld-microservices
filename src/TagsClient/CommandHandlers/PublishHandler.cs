using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using Optional;
using Optional.Async.Extensions;
using TagsClient.Commands;
using TagsClient.gRPCClient;
using TagsService.Protos;
using YngStrs.Common;

namespace TagsClient.CommandHandlers
{
    public class PublishHandler : IRequestHandler<PublishCollection, Option<TagsView, Error>>
    {
        private readonly IServiceClientFactory _clientFactory;

        public PublishHandler(IServiceClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<Option<TagsView, Error>> Handle(
            PublishCollection command,
            CancellationToken cancellationToken) =>
            GetClientOrErrorAsync().FlatMapAsync(client =>
            PublishAsyncOrError(client, command.ToGrpcCmd()));

        private Task<Option<TagService.TagServiceClient, Error>> GetClientOrErrorAsync()
        {
            var result = _clientFactory.Create().Some<TagService.TagServiceClient, Error>();
            return Task.FromResult(result);
        }

        private static async Task<Option<TagsView, Error>> PublishAsyncOrError(
            TagService.TagServiceClient client,
            PublishTags command)
        {
            try
            {
                var result = await client.PublishCollectionAsync(command);
                return result.Some<TagsView, Error>();
            }
            catch (RpcException)
            {
                return Option.None<TagsView, Error>(
                    Error.Critical("An unhandled exception occured while publishing the article's tags."));
            }
        }
    }
}