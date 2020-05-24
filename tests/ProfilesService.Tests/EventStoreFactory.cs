using System.Linq;
using Marten;
using ProfilesService.Domain.Aggregates;
using ProfilesService.Domain.Events;

namespace ProfilesService.Tests
{
    public class EventStoreFactory
    {
        private const string ConnectionString = "PORT = 5432; HOST = localhost; TIMEOUT = 15; POOLING = True; " +
                                                "MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; " +
                                                "DATABASE = 'profiles-tests-event-store'; PASSWORD = 'postgres'; " +
                                                "USER ID = 'postgres'";

        public void EnsureEventStoreIsCreated()
        {
            DocumentStore.For(options =>
            {
                options.Connection(ConnectionString);
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

        public IDocumentSession DocumentSession()
        {
            var documentStore = DocumentStore.For(options =>
            {
                const string schemaName = "public";

                options.Connection(ConnectionString);
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

            var session = documentStore.OpenSession();
            return session;
        }
    }
}