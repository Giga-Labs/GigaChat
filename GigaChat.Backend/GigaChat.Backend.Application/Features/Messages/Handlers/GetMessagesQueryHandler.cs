using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Application.Features.Messages.Queries;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Messages.Handlers;

public class GetMessagesQueryHandler(IMessageRepository messageRepository, IConversationMemberRepository memberRepository) : IRequestHandler<GetMessagesQuery, Result<IReadOnlyList<MessageResponse>>>
{
    public async Task<Result<IReadOnlyList<MessageResponse>>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var isMember = await memberRepository.IsMemberAsync(request.RequesterId, request.ConversationId, cancellationToken);
        if (!isMember)
            return Result.Failure<IReadOnlyList<MessageResponse>>(ConversationErrors.AccessDenied);

        var messages = await messageRepository
            .GetVisibleMessagesForUserAsync(request.RequesterId, request.ConversationId, take: 50, skip: 0, cancellationToken);

        var response = messages.Select(m => new MessageResponse(
            m.Id,
            m.ConversationId,
            m.SenderId,
            m.Content,
            m.CreatedAt
        )).ToList();

        return Result.Success<IReadOnlyList<MessageResponse>>(response);
    }
}