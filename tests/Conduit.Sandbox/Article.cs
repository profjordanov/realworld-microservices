using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Sandbox
{
    public class Article
    {
        public Article()
        {
            
        }

        public Article(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; protected set; }

        public int Version { get; protected set; }

        public string Slug { get; set; }

        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();

        public HashSet<string> UserIdFavorites { get; set; } = new HashSet<string>();

        public ArticlePublished Publish() => new ArticlePublished
        {
            Slug = Slug
        };

        public void Apply(ArticlePublished @event)
        {
            Slug = @event.Slug;
            Version++;
        }

        public void Apply(CommentPublished @event)
        {
            Id = @event.ArticleId;
            Comments.Add(new Comment());
            Version++;
        }
    }
}