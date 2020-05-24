using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using ProfilesClient.Commands;
using ProfilesClient.Hubs;

namespace ProfilesClient.Dispatchers
{
    public class FollowUserDispatcher : INotificationHandler<FollowUser>
    {
        private readonly IHubContext<FollowUserHub> _hubContext;

        public FollowUserDispatcher(IHubContext<FollowUserHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task Handle(FollowUser notification, CancellationToken cancellationToken) =>
            _hubContext
                .Clients
                .All
                .SendAsync(nameof(FollowUser), notification, cancellationToken);
    }
}