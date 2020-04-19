using System.Linq;
using Marten;
using Marten.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Orders.Api.Configuration
{
    public static class DependenciesConfiguration
    {
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

                //options.Events.InlineProjections.AggregateStreamsWith<Tab>();
                //options.Events.InlineProjections.Add(new TabViewProjection());

                //var events = typeof(TabOpened)
                //    .Assembly
                //    .GetTypes()
                //    .Where(t => typeof(IEvent).IsAssignableFrom(t))
                //    .ToList();

                //options.Events.AddEventTypes(events);
            });

            services.AddSingleton<IDocumentStore>(documentStore);

            services.AddScoped(sp => sp.GetService<IDocumentStore>().OpenSession());
        }
    }
}