using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Queries;

public record GetConversationByIdQuery
(
  string RequesterId,
  Guid ConversationId
) : IRequest<Result<ConversationResponse>>;