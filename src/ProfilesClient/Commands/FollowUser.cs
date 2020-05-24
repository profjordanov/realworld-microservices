using MediatR;

namespace ProfilesClient.Commands
{
    public class FollowUser : INotification
    {
        public string FollowerId { get; set; }

        public string FollowingId { get; set; }
    }
}