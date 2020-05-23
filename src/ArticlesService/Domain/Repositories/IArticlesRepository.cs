using System.Collections.Generic;
using System.Threading.Tasks;
using ArticlesService.Domain.Entities;
using Optional;
using YngStrs.Common;

namespace ArticlesService.Domain.Repositories
{
    public interface IArticlesRepository
    {
        Task<Option<Article, Error>> GetBySlugOrErrorAsync(string slug);

        Task<Article> PersistAsync(Article article);

        IAsyncEnumerable<Article> All { get; }
    }
}