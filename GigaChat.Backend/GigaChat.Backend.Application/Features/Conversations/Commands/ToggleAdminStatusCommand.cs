using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Commands;

public record ToggleAdminStatusCommand
(    
    string RequesterId,
    Guid ConversationId,
    string TargetUserId
) : IRequest<Result<ToggleAdminStatusResponse>>;