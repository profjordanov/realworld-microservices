using Conduit.Sandbox;

namespace ArticlesService.Domain.Events
{
    public class ArticlePublished : IArticleEvent
    {
        public string Slug { get; set; }
    }
}