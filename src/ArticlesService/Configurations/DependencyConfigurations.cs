using System;
using ArticlesService.Domain.Repositories;
using ArticlesService.Persistence;
using ArticlesService.Persistence.EntityFramework;
using ArticlesService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesService.Configurations
{
    public static class DependencyConfigurations
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddScoped<IArticlesRepository, ArticlesRepository>();
            return collection;
        }

        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            services.AddDbContext<ArticlesDbContext>(opts =>
                opts.UseNpgsql(connectionString).EnableSensitiveDataLogging());
        }
    }
}