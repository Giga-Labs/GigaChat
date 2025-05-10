using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Block.Commands;
using GigaChat.Backend.Application.Features.Block.Contracts;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Entities.Core;
using MediatR;

namespace GigaChat.Backend.Application.Features.Block.Handlers;

public class ToggleBlockCommandHandler(IBlockedUserRepository blockedUserRepository) : IRequestHandler<ToggleBlockCommand, Result<ToggleBlockResponse>>
{
    public async Task<Result<ToggleBlockResponse>> Handle(ToggleBlockCommand request, CancellationToken cancellationToken)
    {
        if (request.RequesterId == request.TargetUserId)
        {
            return Result.Failure<ToggleBlockResponse>(BlockErrors.SelfBlock);
        }

        var alreadyBlocked = await blockedUserRepository.ExistsAsync(request.RequesterId, request.TargetUserId, cancellationToken);

        if (alreadyBlocked)
        {
            var block = await blockedUserRepository.GetAsync(request.RequesterId, request.TargetUserId, cancellationToken);
            if (block is not null)
            {
                await blockedUserRepository.RemoveAsync(block, cancellationToken);
            }

            return Result.Success(new ToggleBlockResponse(request.TargetUserId, false));
        }

        var newBlock = new BlockedUser
        {
            UserId = request.RequesterId,
            BlockedUserId = request.TargetUserId,
            BlockedAt = DateTime.UtcNow
        };

        await blockedUserRepository.AddAsync(newBlock, cancellationToken);
        return Result.Success(new ToggleBlockResponse(request.TargetUserId, true));
    }
}