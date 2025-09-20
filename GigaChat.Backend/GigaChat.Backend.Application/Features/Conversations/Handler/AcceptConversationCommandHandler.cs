using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Features.Hubs.Commands;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class AcceptConversationCommandHandler(IConversationMemberRepository conversationMemberRepository, IConversationInviteLogRepository conversationInviteLogRepository, IMediator mediator, IConversationConnectionTracker connectionTracker) : IRequestHandler<AcceptConversationCommand, Result>
{
    public async Task<Result> Handle(AcceptConversationCommand request, CancellationToken cancellationToken)
    {
        var invite = await conversationInviteLogRepository
            .GetByUserAndConversationAsync(request.RequesterId, request.ConversationId, cancellationToken);

        if (invite is null)
            return Result.Failure(ConversationErrors.InviteNotFound);

        if (request.Accept)
        {
            invite.IsAccepted = true;
            invite.UpdatedAt = DateTime.UtcNow;
            invite.UpdatedById = request.RequesterId;

            await conversationInviteLogRepository.UpdateAsync(invite, cancellationToken);
        }
        else
        {
            var member = await conversationMemberRepository
                .FindAsync(request.RequesterId, request.ConversationId, cancellationToken);

            if (member is not null)
                await conversationMemberRepository.RemoveAsync(member, cancellationToken);

            await conversationInviteLogRepository.RemoveAsync(invite, cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.ConnectionId))
            {
                await mediator.Send(new UnsubscribeFromConversationCommand(
                    request.ConnectionId,
                    request.ConversationId), cancellationToken);
            }

            connectionTracker.Remove(request.ConnectionId, request.ConversationId);
        }

        return Result.Success();
    }
}