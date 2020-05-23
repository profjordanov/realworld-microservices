using System;

namespace ArticlesService.Domain.Entities
{
    public class ArticleFavorite
    {
        public Guid ArticleId { get; set; }

        public Guid UserId { get; set; }

        public Article Article { get; set; }
    }
}