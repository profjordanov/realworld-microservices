using System;

namespace Conduit.Sandbox
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public Guid ArticleId { get; set; }


        public CommentPublished Add() => new CommentPublished
        {
            ArticleId = ArticleId
        };
    }
}