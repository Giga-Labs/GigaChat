using GigaChat.Backend.Application.Features.Hubs.Commands;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Hubs.Handlers;

public class SubscribeToConversationUpdatesCommandHandler(IConversationHubNotifier conversationHubNotifier) : IRequestHandler<SubscribeToConversationUpdatesCommand, Result>
{
    public async Task<Result> Handle(SubscribeToConversationUpdatesCommand request, CancellationToken cancellationToken)
    {
        await conversationHubNotifier.AddPrivateConnectionAsync(request.ConnectionId, request.UserId);
        
        return Result.Success();
    }
}