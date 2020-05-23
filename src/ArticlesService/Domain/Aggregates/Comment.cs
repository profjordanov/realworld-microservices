using System;
using System.ComponentModel.DataAnnotations;

namespace ArticlesService.Domain.Aggregates
{
    public class Comment
    {
        public Guid Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTimeOffset CreatedAtUtc { get; set; }

        public DateTimeOffset? UpdatedAtUtc { get; set; }

        public string AuthorId { get; set; }

        public int ArticleId { get; set; }
    }
}