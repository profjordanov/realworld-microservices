using System;
using System.Threading.Tasks;
using ArticlesService.Domain.Entities;
using ArticlesService.Domain.Repositories;
using ArticlesService.Protos;
using AutoMapper;
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
            var entity = _repository.AddOrDefault(model);

            var result = _mapper.Map<ArticleView>(entity);
            return Task.FromResult(result);
        }
    }
}