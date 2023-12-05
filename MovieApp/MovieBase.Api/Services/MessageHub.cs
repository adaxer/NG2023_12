using Microsoft.AspNetCore.SignalR;

namespace MovieBase.Api.Services;

public class MessageHub : Hub
{
    public async Task ClientMessage(string message)
    {
        await Clients.All.SendAsync("message", message);
    }
}
