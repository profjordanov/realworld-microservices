using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesService.Domain.Aggregates;
using ArticlesService.Domain.Repositories;
using ArticlesService.Protos;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace ArticlesService.Services
{
    public class ArticlesService : ArticleService.ArticleServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IArticlesRepository _repository;

        public ArticlesService(IMapper mapper, IArticlesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override Task<ArticleView> Publish(PublishArticle request, ServerCallContext context)
        {
            var model = _mapper.Map<Article>(request);

            model.Id = Guid.NewGuid();
            if (_repository.All.Any(article => article.Slug == model.Slug))
            {
                var guidPart = Guid.NewGuid().ToString().Substring(23);
                model.Slug += guidPart;
            }

            var entity = _repository.AddOrDefault(model);

            var result = _mapper.Map<ArticleView>(entity);
            return Task.FromResult(result);
        }

        public override Task<ArticlesView> GetAll(Empty request, ServerCallContext context)
        {
            var results = _mapper.Map<IEnumerable<ArticleView>>(_repository.All);
            return Task.FromResult(new ArticlesView
            {
                Items = { results }
            });
        }

        public override Task<ArticleView> GetBySlug(BySlug request, ServerCallContext context)
        {
            var entity = _repository.All
                .SingleOrDefault(article => article.Slug == request.Slug);

            if (entity == null)
            {
                return null;
            }

            var result = _mapper.Map<ArticleView>(entity);

            return Task.FromResult(result);
        }
    }
}