namespace ProfilesService.Domain.Entities
{
    public class FollowedProfile
    {
        public string FollowerId { get; set; }

        public Profile Follower { get; set; }


        public string FollowingId { get; set; }

        public Profile Following { get; set; }
    }
}