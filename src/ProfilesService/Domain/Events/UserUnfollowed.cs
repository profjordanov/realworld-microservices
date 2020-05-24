namespace ProfilesService.Domain.Events
{
    public class UserUnfollowed : IProfileEvent
    {
        public string FollowerId { get; set; }

        public string FollowingId { get; set; }
    }
}