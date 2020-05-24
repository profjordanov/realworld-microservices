using System.Linq;
using System.Reflection;
using Marten;
using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfilesService.Domain.Aggregates;
using ProfilesService.Domain.Events;

namespace ProfilesService.Configurations
{
    public static class DependencyConfigurations
    {
        public static IServiceCollection AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            var documentStore = GetDocumentStoreByConfiguration(configuration);

            services.AddSingleton<IDocumentStore>(documentStore);

            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());

            return services;
        }

        public static DocumentStore GetDocumentStoreByConfiguration(IConfiguration configuration)
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

                options.Events.InlineProjections.AggregateStreamsWith<Follower>();

                var events = typeof(UserFollowed)
                    .Assembly
                    .GetTypes()
                    .Where(t => typeof(IProfileEvent).IsAssignableFrom(t))
                    .ToList();

                options.Events.AddEventTypes(events);
            });

            return documentStore;
        }

        public static void EnsureEventStoreIsCreated(this IConfiguration configuration)
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