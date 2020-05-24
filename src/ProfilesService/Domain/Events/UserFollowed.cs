namespace ProfilesService.Domain.Events
{
    public class UserFollowed : IProfileEvent
    {
        public string FollowerId { get; set; }

        public string FollowingId { get; set; }
    }
}