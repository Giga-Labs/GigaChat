using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Messages.Commands;
using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Domain.Enums.Core;
using MediatR;

namespace GigaChat.Backend.Application.Features.Messages.Handlers;

public class SendMessageCommandHandler(IMessageRepository messageRepository, IConversationMemberRepository memberRepository, IMessageBroadcaster broadcaster)
    : IRequestHandler<SendMessageCommand, Result<MessageResponse>>
{
    public async Task<Result<MessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var isMember = await memberRepository.IsMemberAsync(request.SenderId, request.ConversationId, cancellationToken);
        if (!isMember)
            return Result.Failure<MessageResponse>(ConversationErrors.AccessDenied);

        var message = new Message
        {
            Id = Guid.NewGuid(),
            ConversationId = request.ConversationId,
            SenderId = request.SenderId,
            Content = request.Content,
            Type = MessageType.Text,
            CreatedAt = DateTime.UtcNow,
            CreatedById = request.SenderId
        };

        await messageRepository.AddAsync(message, cancellationToken);

        var response = new MessageResponse(
            message.Id,
            message.ConversationId,
            message.SenderId,
            message.Content,
            message.CreatedAt
        );

        await broadcaster.BroadcastNewMessageAsync(request.ConversationId, response);

        return Result.Success(response);
    }
}