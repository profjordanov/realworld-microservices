using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArticlesClient.gRPCClient;
using ArticlesClient.Queries;
using ArticlesClient.Views;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using TagsClient.Queries;

namespace ArticlesClient.QueryHandlers
{
    public class AllArticlesHandler : IRequestHandler<AllArticles, IEnumerable<ArticleProjection>>
    {
        private readonly IServiceClientFactory _clientFactory;
        private readonly IMediator _mediator;

        public AllArticlesHandler(IServiceClientFactory clientFactory, IMediator mediator)
        {
            _clientFactory = clientFactory;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ArticleProjection>> Handle(AllArticles request, CancellationToken cancellationToken)
        {
            var client = _clientFactory.Create();
            var allArticlesAsync = Task.Run(
                () => client.GetAllAsync(new Empty()).ResponseAsync, cancellationToken);
            var tagsView = await _mediator.Send(new AllTags(), cancellationToken);
            var articles = await allArticlesAsync;

            var result = new List<ArticleProjection>();
            foreach (var article in articles.Items)
            {
                var articleTags = tagsView.Tags
                    .Where(t => t.ArticleIds.Contains(article.Id))
                    .Select(t => t.Name)
                    .ToArray();

                var projection = new ArticleProjection
                {
                    Id = Guid.Parse(article.Id),
                    Title = article.Title,
                    Description = article.Description,
                    Body = article.Body,
                    Slug = article.Slug,
                    CreatedAtUtc = article.CreatedAtUtc.ToDateTimeOffset(),
                    UpdatedAtUtc = article.UpdatedAtUtc.ToDateTimeOffset(),
                    TagList = articleTags
                };

                result.Add(projection);
            }

            return result;
        }
    }
}