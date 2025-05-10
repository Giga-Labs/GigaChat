using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class RemoveConversationCommandHandler(IConversationRepository conversationRepository, IConversationMemberRepository conversationMemberRepository, IConversationBroadcaster conversationBroadcaster) : IRequestHandler<RemoveConversationCommand, Result>
{
    public async Task<Result> Handle(RemoveConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.FindByIdAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result.Failure(ConversationErrors.NotFound);

        var isAdmin = await conversationMemberRepository.IsAdminAsync(request.RequesterId, request.ConversationId, cancellationToken);
        if (!isAdmin)
            return Result.Failure(ConversationErrors.AccessDenied);

        var members = await conversationMemberRepository.GetMembersAsync(request.ConversationId, cancellationToken);
        var participantIds = members.Select(m => m.UserId).Distinct().ToList();

        await conversationRepository.RemoveAsync(conversation, cancellationToken);

        var broadcastTasks = participantIds.Select(userId =>
            conversationBroadcaster.BroadcastConversationRemovedAsync(userId, request.ConversationId)
        );

        await Task.WhenAll(broadcastTasks);

        return Result.Success();
    }
}