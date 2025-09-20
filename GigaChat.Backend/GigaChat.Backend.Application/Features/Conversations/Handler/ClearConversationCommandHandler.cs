using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Entities.Core;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class ClearConversationCommandHandler(IClearedConversationRepository clearedConversationRepository, IConversationMemberRepository memberRepository) : IRequestHandler<ClearConversationCommand, Result>
{
    public async Task<Result> Handle(ClearConversationCommand request, CancellationToken cancellationToken)
    {
        var isMember = await memberRepository.IsMemberAsync(request.UserId, request.ConversationId, cancellationToken);
        if (!isMember)
            return Result.Failure(ConversationErrors.AccessDenied);

        var cleared = new ClearedConversation
        {
            UserId = request.UserId,
            ConversationId = request.ConversationId,
            ClearedAt = DateTime.UtcNow
        };

        await clearedConversationRepository.AddAsync(cleared, cancellationToken);

        return Result.Success();
    }
}