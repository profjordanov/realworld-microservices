using Microsoft.AspNetCore.SignalR.Client;

namespace ProfilesService.HubConnections
{
    public class FollowerHubConnection
    {
        private readonly HubConnection _connection;

        public FollowerHubConnection(string url, string accessToken)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }
    }
}