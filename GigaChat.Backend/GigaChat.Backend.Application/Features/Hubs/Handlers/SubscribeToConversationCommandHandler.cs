using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Hubs.Commands;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Hubs.Handlers;

public class SubscribeToConversationCommandHandler(IConversationMemberRepository conversationMemberRepository, IConversationHubNotifier conversationHubNotifier, IMessageHubNotifier messageHubNotifier, IConversationConnectionTracker tracker) : IRequestHandler<SubscribeToConversationCommand, Result>
{
    public async Task<Result> Handle(SubscribeToConversationCommand request, CancellationToken cancellationToken)
    {
        // check if the user is a member of the conversation 
        var isMember =
            await conversationMemberRepository.IsMemberAsync(request.UserId, request.ConversationId, cancellationToken);
        if (!isMember)
            return Result.Failure(ConversationErrors.AccessDenied);

        await conversationHubNotifier.AddConnectionAsync(request.ConnectionId, request.ConversationId);
        await messageHubNotifier.AddConnectionAsync(request.ConnectionId, request.ConversationId);
        
        tracker.Add(request.ConnectionId, request.ConversationId);
        
        return Result.Success();
    }
}