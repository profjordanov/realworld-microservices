using System;

namespace ArticlesClient.Views
{
    public class ArticleProjection
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        //public string[] TagList { get; set; }

        //public DateTime CreatedAt { get; set; }

        //public DateTime UpdatedAt { get; set; }

        //public bool Favorited { get; set; } //Favorited

        //public int FavoritesCount { get; set; }

        //public UserProfileModel Author { get; set; }
    }
}