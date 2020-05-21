using System;
using System.Threading;
using System.Threading.Tasks;
using ArticlesClient.Commands;
using ArticlesClient.gRPCClient;
using ArticlesClient.Views;
using Grpc.Core;
using MediatR;
using Optional;
using YngStrs.Common;

namespace ArticlesClient.CommandHandlers
{
    public class PublishHandler : IRequestHandler<Publish, Option<ArticleProjection, Error>>
    {
        private readonly IServiceClientFactory _clientFactory;

        public PublishHandler(IServiceClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Option<ArticleProjection, Error>> Handle(Publish command, CancellationToken cancellationToken)
        {
            try
            {
                var client = _clientFactory.Create();
                var view = await client.PublishAsync(command.ToPublishArticle());
                var projection = new ArticleProjection
                {
                    Id = Guid.Parse(view.Id),
                    Title = view.Title,
                    Description = view.Description,
                    Body = view.Body,
                    Slug = view.Slug
                };
                return projection.Some<ArticleProjection, Error>();
            }
            catch (RpcException)
            {
                return Option.None<ArticleProjection, Error>(
                    Error.Critical("An unhandled exception occured while persisting the article."));
            }
        }
    }
}