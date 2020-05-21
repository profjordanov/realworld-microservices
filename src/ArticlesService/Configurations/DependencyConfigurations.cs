using ArticlesService.Domain.Repositories;
using ArticlesService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesService.Configurations
{
    public static class DependencyConfigurations
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddSingleton<IArticlesRepository, ArticlesRepository>();
            return collection;
        }
    }
}