

using Marten;

namespace ProfilesService.Persistence.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDocumentSession _session;

        public ProfilesRepository(IDocumentSession session)
        {
            _session = session;
        }

    }
}