using System;

namespace Conduit.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventStore = new EventStore();
            var documentSession = eventStore.DocumentSession();

            var aggId = Guid.NewGuid();
            var aggregate = new Article(aggId)
            {
                Slug = "slug"
            };

            var streamId = Guid.NewGuid();
            documentSession.Events.Append(streamId, aggregate.Publish());
            documentSession.SaveChanges();


            var comment = new Comment {ArticleId = aggId};
            documentSession.Events.Append(streamId, comment.Add());
            documentSession.SaveChanges();

        }
    }
}
