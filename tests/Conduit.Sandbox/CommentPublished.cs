using System;

namespace Conduit.Sandbox
{
    // CommentAdded
    public class CommentPublished : IArticleEvent
    {
        public Guid ArticleId { get; set; }
    }
}