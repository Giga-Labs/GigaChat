using GigaChat.Backend.Application.Features.Block.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Block.Commands;

public record ToggleBlockCommand
(
    string RequesterId,
    string TargetUserId
) : IRequest<Result<ToggleBlockResponse>>;