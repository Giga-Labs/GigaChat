using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Commands;

public record ClearConversationCommand(Guid ConversationId, string UserId) : IRequest<Result>;