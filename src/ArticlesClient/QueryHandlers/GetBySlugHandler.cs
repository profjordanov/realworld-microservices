using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ArticlesClient.gRPCClient;
using ArticlesClient.Queries;
using ArticlesClient.Views;
using MediatR;
using TagsClient.Queries;

namespace ArticlesClient.QueryHandlers
{
    public class GetBySlugHandler : IRequestHandler<GetBySlug, ArticleProjection>
    {
        private readonly IServiceClientFactory _clientFactory;
        private readonly IMediator _mediator;

        public GetBySlugHandler(IServiceClientFactory clientFactory, IMediator mediator)
        {
            _clientFactory = clientFactory;
            _mediator = mediator;
        }

        public async Task<ArticleProjection> Handle(GetBySlug query, CancellationToken cancellationToken)
        {
            var client = _clientFactory.Create();
            var article = await client.GetBySlugAsync(query.ToGrpcQuery);
            if (article == null)
            {
                return null;
            }

            var tags = await _mediator.Send(new AllByArticleId(article.Id), cancellationToken);
            var tagNames = tags?.Tags?.Select(view => view.Name).ToArray();

            return new ArticleProjection
            {
                Id = Guid.Parse(article.Id),
                Title = article.Title,
                Description = article.Description,
                Body = article.Body,
                CreatedAtUtc = article.CreatedAtUtc.ToDateTimeOffset(),
                UpdatedAtUtc = article.UpdatedAtUtc.ToDateTimeOffset(),
                Slug = article.Slug,
                TagList = tagNames
            };
        }
    }
}