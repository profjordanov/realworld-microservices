using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesService.Domain.Entities;
using ArticlesService.Domain.Repositories;
using ArticlesService.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Async.Extensions;
using YngStrs.Common;

namespace ArticlesService.Persistence.Repositories
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly ArticlesDbContext _dbContext;

        public ArticlesRepository(ArticlesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> HasAnyBySlugAsync(string slug) =>
            _dbContext
                .Articles
                .AnyAsync(article => article.Slug == slug);

        public Task<Option<Article, Error>> GetBySlugOrErrorAsync(string slug) =>
            _dbContext
                .Articles
                .AsNoTracking()
                .SingleOrDefaultAsync(article => article.Slug == slug)
                .SomeNotNullAsync(Error.NotFound($"No article found with slug: {slug}."));

        public async Task<Article> PersistAsync(Article article)
        {
            var hasSimilarSlug = await HasAnyBySlugAsync(article.Slug);
            if (hasSimilarSlug)
            {
                var guidPart = Guid.NewGuid().ToString().Substring(23);
                article.Slug += guidPart;
            }

            await _dbContext.Articles.AddAsync(article);
            await _dbContext.SaveChangesAsync();

            return article;
        }

        public IAsyncEnumerable<Article> All =>
            _dbContext
                .Articles
                .AsNoTracking()
                .AsAsyncEnumerable();
    }
}