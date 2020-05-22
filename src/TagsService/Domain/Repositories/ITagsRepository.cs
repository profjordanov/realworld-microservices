using System.Collections.Generic;
using TagsService.Domain.Entities;

namespace TagsService.Domain.Repositories
{
    public interface ITagsRepository
    {
        IEnumerable<Tag> All { get; }

        Tag AddOrDefault(Tag tag);
    }
}