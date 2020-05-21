using System;

namespace TagsService.Domain.Entities
{
    public class ArticleTag
    {
        public Guid ArticleId { get; set; }

        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}