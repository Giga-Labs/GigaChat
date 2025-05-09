using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class AcceptConversationCommandHandler(IConversationMemberRepository conversationMemberRepository, IConversationInviteLogRepository conversationInviteLogRepository) : IRequestHandler<AcceptConversationCommand, Result>
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
            // Remove from members
            var member = await conversationMemberRepository
                .FindAsync(request.RequesterId, request.ConversationId, cancellationToken);

            if (member is not null)
                await conversationMemberRepository.RemoveAsync(member, cancellationToken);

            // Remove invite
            await conversationInviteLogRepository.RemoveAsync(invite, cancellationToken);
        }

        return Result.Success();
    }
}