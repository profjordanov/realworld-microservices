using System;
using ProfilesService.Domain.Events;

namespace ProfilesService.Domain.Aggregates
{
    public class Follower
    {
        public Guid Id { get; set; }

        public string FollowerId { get; set; }

        public string FollowingId { get; set; }

        public UserFollowed Follow() => new UserFollowed
        {
            FollowingId = FollowingId,
            FollowerId = FollowerId
        };

        public void Apply(UserFollowed @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));
            Id = Guid.NewGuid();
            FollowingId = @event.FollowingId;
            FollowerId = @event.FollowerId;
        }

        public UserUnfollowed Unfollow() => new UserUnfollowed
        {
            FollowingId = FollowingId,
            FollowerId = FollowerId
        };

        public void Apply(UserUnfollowed @event)
        {
            //registers document deletion
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));
        }
    }
}