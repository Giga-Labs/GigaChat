using GigaChat.Backend.Application.Features.Hubs.Commands;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Hubs.Handlers;

public class UnsubscribeFromConversationCommandHandler(IConversationHubNotifier conversationHubNotifier, IMessageHubNotifier messageHubNotifier) : IRequestHandler<UnsubscribeFromConversationCommand, Result>
{
    public async Task<Result> Handle(UnsubscribeFromConversationCommand request, CancellationToken cancellationToken)
    {
        await conversationHubNotifier.RemoveConnectionAsync(request.ConnectionId, request.ConversationId);
        await messageHubNotifier.RemoveConnectionAsync(request.ConnectionId, request.ConversationId);

        return Result.Success();
    }
}