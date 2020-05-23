using System.Collections.Generic;
using ArticlesService.Domain.Aggregates;

namespace ArticlesService.Domain.Repositories
{
    public interface IArticlesRepository
    {
        IEnumerable<Article> All { get; }

        Article AddOrDefault(Article article);
    }
}