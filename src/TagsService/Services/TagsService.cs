using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using TagsService.Domain.Entities;
using TagsService.Persistence.Repositories;
using TagsService.Protos;

namespace TagsService.Services
{
    public class TagsService : TagService.TagServiceBase
    {
        public override Task<TagsView> PublishCollection(PublishTags request, ServerCallContext context)
        {
            var repo = new TagsRepository();
            var tagViews = new RepeatedField<TagView>();
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
                repo.AddOrDefault(entity);
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
    }
}