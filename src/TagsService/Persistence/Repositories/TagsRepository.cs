using System.Collections.Concurrent;
using System.Collections.Generic;
using TagsService.Domain.Entities;

namespace TagsService.Persistence.Repositories
{
    public class TagsRepository
    {
        private static readonly List<KeyValuePair<int, Tag>> KeyValuePairs = new List<KeyValuePair<int, Tag>>
        {
            new KeyValuePair<int, Tag>(1, new Tag
            {
                Id = 1
            })
        };

        private readonly ConcurrentDictionary<int, Tag> _dictionary =
            new ConcurrentDictionary<int, Tag>(KeyValuePairs);

        public IEnumerable<Tag> All => _dictionary.Values;

        public Tag GetByIdOrDefault(int tagId)
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