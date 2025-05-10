using GigaChat.Backend.Application.Features.Block.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Block.Queries;

public record GetBlockedUsersQuery(string RequesterId) : IRequest<Result<IReadOnlyList<BlockedUserResponse>>>;