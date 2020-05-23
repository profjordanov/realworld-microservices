using ArticlesService.Domain.Repositories;
using ArticlesService.Persistence.Repositories;
using Conduit.Sandbox;
using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ArticlesService.Domain.Aggregates;

namespace ArticlesService.Configurations
{
    public static class DependencyConfigurations
    {
        public static IServiceCollection AddRepositories(this IServiceCollection collection)
        {
            collection.AddSingleton<IArticlesRepository, ArticlesRepository>();
            return collection;
        }

        public static void AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            var documentStore = DocumentStore.For(options =>
            {
                var config = configuration.GetSection("EventStore");
                var connectionString = config.GetValue<string>("ConnectionString");
                var schemaName = config.GetValue<string>("Schema");

                options.Connection(connectionString);
                options.AutoCreateSchemaObjects = AutoCreate.All;
                options.Events.DatabaseSchemaName = schemaName;
                options.DatabaseSchemaName = schemaName;

                options.Events.InlineProjections.AggregateStreamsWith<Article>();
                
                var events = typeof(Startup)
                    .Assembly
                    .GetTypes()
                    .Where(t => typeof(IArticleEvent).IsAssignableFrom(t))
                    .ToList();

                options.Events.AddEventTypes(events);
            });

            services.AddSingleton<IDocumentStore>(documentStore);

            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());
        }

        public static void EnsureEventStoreIsCreated(IConfiguration configuration)
        {
            DocumentStore.For(options =>
            {
                options.Connection(configuration.GetSection("EventStore")["ConnectionString"]);
                options.CreateDatabasesForTenants(c =>
                {
                    c.ForTenant()
                        .CheckAgainstPgDatabase()
                        .WithOwner("postgres")
                        .WithEncoding("UTF-8")
                        .ConnectionLimit(-1);
                });
            });
        }
    }
}