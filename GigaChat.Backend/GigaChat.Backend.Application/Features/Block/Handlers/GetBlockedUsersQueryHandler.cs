using GigaChat.Backend.Application.Features.Block.Contracts;
using GigaChat.Backend.Application.Features.Block.Queries;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Block.Handlers;

public class GetBlockedUsersQueryHandler(IBlockedUserRepository blockedUserRepository, IUserRepository userRepository) : IRequestHandler<GetBlockedUsersQuery, Result<IReadOnlyList<BlockedUserResponse>>>
{
    public async Task<Result<IReadOnlyList<BlockedUserResponse>>> Handle(GetBlockedUsersQuery request, CancellationToken cancellationToken)
    {
        var blockedUsers = await blockedUserRepository.GetBlockedUsersAsync(request.RequesterId, cancellationToken);

        var responses = new List<BlockedUserResponse>();

        foreach (var blocked in blockedUsers)
        {
            var user = await userRepository.FindByIdAsync(blocked.BlockedUserId);
            if (user is null) continue;

            responses.Add(new BlockedUserResponse(
                user.Id,
                user.UserName,
                user.Email,
                blocked.BlockedAt
            ));
        }

        return Result.Success<IReadOnlyList<BlockedUserResponse>>(responses);
    }
}