using System;
using System.Threading.Tasks;
using Marten;
using ProfilesService.Domain.Aggregates;

namespace ProfilesService.Persistence.Repositories
{
    public class FollowersRepository
    {
        private readonly IDocumentSession _session;

        public FollowersRepository(IDocumentSession session)
        {
            _session = session;
        }

        public Task<Follower> GetAsync(string followerId, string followingId) => _session
            .Query<Follower>()
            .FirstOrDefaultAsync(aggregate =>
                aggregate.FollowerId == followerId && aggregate.FollowingId == followingId);

        public Task<bool> HasAnyAsync(string followerId, string followingId) => _session
            .Query<Follower>()
            .AnyAsync(agg => 
                agg.FollowerId == followerId && agg.FollowingId == followingId);

        public async Task FollowAsync(Follower aggregate)
        {
            _session.Events.Append(
                stream: Guid.Parse(aggregate.FollowerId),
                events: aggregate.Follow());

            await _session.SaveChangesAsync();
        }

        public async Task UnfollowAsync(Follower aggregate)
        {
            _session.Events.Append(
                stream: Guid.Parse(aggregate.FollowerId),
                events: aggregate.Unfollow());

            await _session.SaveChangesAsync();
            await RemoveAsync(aggregate);
        }

        public Task RemoveAsync(Follower aggregate)
        {
            _session.Delete(aggregate);
            return _session.SaveChangesAsync();
        }
    }
}