using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Commands;

public record RemoveConversationCommand(string RequesterId, Guid ConversationId) : IRequest<Result>;