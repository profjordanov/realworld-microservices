using System;
using System.ComponentModel.DataAnnotations;

namespace ArticlesService.Domain.Aggregates
{
    public class Article
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public DateTimeOffset? UpdatedAtUtc { get; set; }

        [Required]
        public string AuthorId { get; set; }

        //public int FavoritesCount => Favorites?.Count ?? 0;

        //public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();

        //public HashSet<ArticleFavorite> Favorites { get; set; } = new HashSet<ArticleFavorite>();
    }
}