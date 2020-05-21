using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using TagsService.Domain.Entities;
using TagsService.Domain.Repositories;
using TagsService.Persistence.Repositories;
using TagsService.Protos;

namespace TagsService.Services
{
    public class TagsService : TagService.TagServiceBase
    {
        private readonly ITagsRepository _repository;

        public TagsService(ITagsRepository repository)
        {
            _repository = repository;
        }

        public override Task<TagsView> PublishCollection(PublishTags request, ServerCallContext context)
        {
            var tagViews = new List<TagView>();
            foreach (var tag in request.Names)
            {
                var tagId = Guid.NewGuid();
                var entity = new Tag
                {
                    Id = tagId,
                    Name = tag,
                    ArticleTags = new HashSet<ArticleTag>
                    {
                        new ArticleTag
                        {
                            TagId = tagId,
                            ArticleId = Guid.Parse(request.ArticleId)
                        }
                    }
                };
                _repository.AddOrDefault(entity);
                var view = new TagView
                {
                    Id = tagId.ToString(),
                    Name = tag
                };
                tagViews.Add(view);
            }
            var result = new TagsView
            {
                Tags = { tagViews }
            };

            return Task.FromResult(result);
        }

        public override Task<TagsView> GetAll(Empty request, ServerCallContext context)
        {
            var results = _repository.All.Select(tag => new TagView
            {
                Id = tag.Id.ToString(),
                Name = tag.Name,
                ArticleIds =
                {
                    tag.ArticleTags.Select(articleTag => articleTag.ArticleId.ToString())
                }
            });

            var result = new TagsView
            {
                Tags = {results}
            };

            return Task.FromResult(result);
        }
    }
}