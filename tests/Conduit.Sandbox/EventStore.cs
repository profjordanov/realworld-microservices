using System.Linq;
using Marten;

namespace Conduit.Sandbox
{
    public class EventStore
    {
        public IDocumentSession DocumentSession()
        {
            var documentStore = DocumentStore.For(options =>
            {
                var config = ""; //configuration.GetSection("EventStore");
                var connectionString = "PORT = 5432; HOST = localhost; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'articles-event-store'; PASSWORD = 'postgres'; USER ID = 'postgres'";// config.GetValue<string>("ConnectionString");
                var schemaName = "public";// config.GetValue<string>("Schema");

                options.Connection(connectionString);
                options.AutoCreateSchemaObjects = AutoCreate.All;
                options.Events.DatabaseSchemaName = schemaName;
                options.DatabaseSchemaName = schemaName;

                options.Events.InlineProjections.AggregateStreamsWith<Article>();

                var events = typeof(ArticlePublished)
                    .Assembly
                    .GetTypes()
                    .Where(t => typeof(IArticleEvent).IsAssignableFrom(t))
                    .ToList();

                options.Events.AddEventTypes(events);
            });

            var session = documentStore.OpenSession();
            return session;
        }
    }
}