using ArticlesService.Domain.Entities;

namespace ArticlesService.Domain.Repositories
{
    public interface IArticlesRepository
    {
        Article AddOrDefault(Article article);
    }
}