using System;
using System.ComponentModel.DataAnnotations;

namespace ArticlesService.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTimeOffset CreatedAtUtc { get; set; }

        public DateTimeOffset? UpdatedAtUtc { get; set; }

        public string AuthorId { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}