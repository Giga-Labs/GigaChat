using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Backend.Api.Hubs;

[Authorize]
public class MessageHub(IMediator mediator) : Hub
{
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        // await mediator.Send(new UnsubscribeFromConversationCommand(Context.ConnectionId));
        return base.OnDisconnectedAsync(exception);
    }
    
}