using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Queries;

public record GetAccessibleConversationsQuery(string RequesterId) : IRequest<Result<IReadOnlyList<ConversationResponse>>>;