using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Messages.Queries;

public record GetMessagesQuery(string RequesterId, Guid ConversationId) : IRequest<Result<IReadOnlyList<MessageResponse>>>;