using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Enums.Core;
using MediatR;

namespace GigaChat.Backend.Application.Features.Messages.Commands;

public record SendMessageCommand(Guid ConversationId, string SenderId, string Content) : IRequest<Result<MessageResponse>>;