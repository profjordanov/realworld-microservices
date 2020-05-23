using System;

namespace Conduit.Sandbox
{
    // Comment Added
    public class CommentPublished : IArticleEvent
    {
        public Guid ArticleId { get; set; }
    }
}