using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using TagsService.Domain.Entities;
using TagsService.Domain.Repositories;

namespace TagsService.Persistence.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private static readonly List<KeyValuePair<Guid, Tag>> KeyValuePairs = new List<KeyValuePair<Guid, Tag>>
        {
            new KeyValuePair<Guid, Tag>(Guid.Parse("47563f41-4c43-473c-83a5-6e65770481c7"), new Tag
            {
                Id = Guid.Parse("47563f41-4c43-473c-83a5-6e65770481c7"),
                Name = "Name",
                ArticleTags = new HashSet<ArticleTag>
                {
                    new ArticleTag
                    {
                        ArticleId = Guid.Parse("1ba6663f-cba5-44ad-8ef6-81f7500e1351"),
                        TagId = Guid.Parse("47563f41-4c43-473c-83a5-6e65770481c7")
                    }
                }
            })
        };

        private readonly ConcurrentDictionary<Guid, Tag> _dictionary =
            new ConcurrentDictionary<Guid, Tag>(KeyValuePairs);

        public IEnumerable<Tag> All => _dictionary.Values;

        public Tag GetByIdOrDefault(Guid tagId)
        {
            var hasValue = _dictionary.TryGetValue(tagId, out var Tag);
            return hasValue ? Tag : default;
        }

        public Tag AddOrDefault(Tag tag)
        {
            var added = _dictionary.TryAdd(tag.Id, tag);
            return added ? tag : default;
        }

        public Tag UpdateOrDefault(Tag tag)
        {
            var hasValue = _dictionary.TryGetValue(tag.Id, out _);

            if (!hasValue)
            {
                return default;
            }

            _dictionary[tag.Id] = tag;
            return tag;
        }
    }
}