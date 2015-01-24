using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace WebTorrent.WebApp.Hubs
{
    [HubName("AppHub")]
    public class AppHub : Hub
    {
    }
}