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
            var response = await client.GetBySlugAsync(query.ToGrpcQuery);
            if (!response.HasResult)
            {
                return null;
            }

            var result = new ArticleProjection
            {
                Id = Guid.Parse(response.View.Id),
                Title = response.View.Title,
                Description = response.View.Description,
                Body = response.View.Body,
                CreatedAtUtc = response.View.CreatedAtUtc.ToDateTimeOffset(),
                UpdatedAtUtc = response.View.UpdatedAtUtc.ToDateTimeOffset(),
                Slug = response.View.Slug
            };

            var tagsView = await _mediator.Send(new AllByArticleId(response.View.Id), cancellationToken);
            if (tagsView.Tags == null || tagsView.Tags.All(view => view == null))
            {
                return result;
            }

            var tagNames = tagsView.Tags.Select(view => view.Name).ToArray();
            result.TagList = tagNames;
            return result;
        }
    }
}