using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArticlesClient.Commands;
using ArticlesClient.gRPCClient;
using ArticlesClient.Views;
using ArticlesService.Protos;
using Grpc.Core;
using MediatR;
using Optional;
using Optional.Async.Extensions;
using TagsClient.Commands;
using TagsService.Protos;
using YngStrs.Common;

namespace ArticlesClient.CommandHandlers
{
    public class PublishHandler : IRequestHandler<Publish, Option<ArticleProjection, Error>>
    {
        private readonly IServiceClientFactory _clientFactory;
        private readonly IMediator _mediator;

        public PublishHandler(IServiceClientFactory clientFactory, IMediator mediator)
        {
            _clientFactory = clientFactory;
            _mediator = mediator;
        }

        public Task<Option<ArticleProjection, Error>> Handle(
            Publish command,
            CancellationToken cancellationToken) =>
            ClientOrErrorAsync().FlatMapAsync(client => 
            PublishArticleOrErrorAsync(command.ToPublishArticle(), client).FlatMapAsync(view => 
            PublishTagsOrErrorAsync(command.TagList, view.Id).FlatMapAsync(tagsView => 
            ResultOrErrorAsync(view, tagsView))));

        private Task<Option<ArticleService.ArticleServiceClient, Error>> ClientOrErrorAsync()
        {
            var result = _clientFactory.Create().Some<ArticleService.ArticleServiceClient, Error>();
            return Task.FromResult(result);
        }

        private static async Task<Option<ArticleView, Error>> PublishArticleOrErrorAsync(
            PublishArticle article,
            ArticleService.ArticleServiceClient client)
        {
            try
            {
                var view = await client.PublishAsync(article);
                return view.Some<ArticleView, Error>();
            }
            catch (RpcException)
            {
                return Option.None<ArticleView, Error>(
                    Error.Critical("An unhandled exception occured while persisting the article."));
            }
        }

        private async Task<Option<TagsView, Error>> PublishTagsOrErrorAsync(
            string[] tagList,
            string articleId)
        {
            if (tagList == null)
            {
                return Option.Some<TagsView, Error>(null);
            }
            try
            {
                var command = new PublishCollection(tagList, articleId);
                var result = await _mediator.Send(command);
                return result;
            }
            catch (RpcException)
            {
                return Option.None<TagsView, Error>(
                    Error.Critical("An unhandled exception occured while persisting the article's tags."));
            }
        }

        private static Task<Option<ArticleProjection, Error>> ResultOrErrorAsync(
            ArticleView view,
            TagsView tagsView)
        {
            var projection = new ArticleProjection
            {
                Id = Guid.Parse(view.Id),
                Title = view.Title,
                Description = view.Description,
                Body = view.Body,
                Slug = view.Slug,
                CreatedAtUtc = view.CreatedAtUtc.ToDateTimeOffset(),
                UpdatedAtUtc = view.UpdatedAtUtc.ToDateTimeOffset(),
                TagList = tagsView?.Tags?.Select(tag => tag.Name).ToArray()
            };
            var result = projection.Some<ArticleProjection, Error>();
            return Task.FromResult(result);
        } 
    }
}