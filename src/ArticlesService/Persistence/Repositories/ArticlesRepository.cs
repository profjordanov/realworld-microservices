using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ArticlesService.Domain.Aggregates;
using ArticlesService.Domain.Repositories;

namespace ArticlesService.Persistence.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private static readonly List<KeyValuePair<Guid, Article>> KeyValuePairs = new List<KeyValuePair<Guid, Article>>
        {
            new KeyValuePair<Guid, Article>(Guid.Parse("1ba6663f-cba5-44ad-8ef6-81f7500e1351"), new Article
            {
                Id = Guid.Parse("1ba6663f-cba5-44ad-8ef6-81f7500e1351"),
                Title = "Title",
                CreatedAtUtc = DateTimeOffset.UtcNow,
                Description = "Description",
                Body = "Body",
                UpdatedAtUtc = DateTimeOffset.UtcNow,
                AuthorId = "AuthorId",
                Slug = "Slug"
            })
        };

        private readonly ConcurrentDictionary<Guid, Article> _dictionary =
            new ConcurrentDictionary<Guid, Article>(KeyValuePairs);

        public IEnumerable<Article> All => _dictionary.Values;

        public Article GetByIdOrDefault(Guid articleId)
        {
            var hasValue = _dictionary.TryGetValue(articleId, out var Article);
            return hasValue ? Article : default;
        }

        public Article AddOrDefault(Article article)
        {
            var added = _dictionary.TryAdd(article.Id, article);
            return added ? article : default;
        }

        public Article UpdateOrDefault(Article article)
        {
            var hasValue = _dictionary.TryGetValue(article.Id, out _);

            if (!hasValue)
            {
                return default;
            }

            _dictionary[article.Id] = article;
            return article;
        }
    }
}