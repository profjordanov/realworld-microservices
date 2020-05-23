using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticlesService.Domain.Entities
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

        [NotMapped]
        public int FavoritesCount => Favorites?.Count ?? 0;

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<ArticleFavorite> Favorites { get; set; } = new HashSet<ArticleFavorite>();
    }
}