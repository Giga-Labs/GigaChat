using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Models;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Interfaces.Identity;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class ToggleAdminStatusCommandHandler(IConversationMemberRepository memberRepo, IConversationRepository convoRepo, IUserRepository userRepo, IConversationBroadcaster broadcaster) : IRequestHandler<ToggleAdminStatusCommand, Result<ToggleAdminStatusResponse>>
{
    public async Task<Result<ToggleAdminStatusResponse>> Handle(ToggleAdminStatusCommand request, CancellationToken cancellationToken)
    {
        var conversation = await convoRepo.FindByIdAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result.Failure<ToggleAdminStatusResponse>(ConversationErrors.NotFound);

        var requesterMember = await memberRepo.FindAsync(request.RequesterId, request.ConversationId, cancellationToken);
        if (requesterMember is null || !requesterMember.IsAdmin)
            return Result.Failure<ToggleAdminStatusResponse>(ConversationErrors.AccessDenied);

        if (request.RequesterId == request.TargetUserId)
            return Result.Failure<ToggleAdminStatusResponse>(ConversationErrors.CannotModifySelf);

        var targetMember = await memberRepo.FindAsync(request.TargetUserId, request.ConversationId, cancellationToken);
        if (targetMember is null)
            return Result.Failure<ToggleAdminStatusResponse>(ConversationErrors.TargetNotMember);

        targetMember.IsAdmin = !targetMember.IsAdmin;

        await memberRepo.UpdateAsync(targetMember, cancellationToken);

        var members = await memberRepo.GetMembersAsync(request.ConversationId, cancellationToken);
        var users = new List<IApplicationUser>();

        foreach (var member in members)
        {
            var user = await userRepo.FindByIdAsync(member.UserId);
            if (user is not null)
                users.Add(user);
        }

        var updated = new ConversationResponse(
            conversation.Id,
            conversation.Name ?? "",
            conversation.IsGroup,
            conversation.AdminId,
            users.Select(u =>
                new ReceiverModel(
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    members.First(m => m.UserId == u.Id).IsAdmin,
                    u.ProfilePictureUrl // <- Don't forget this guy
                )
            ).ToList()
        );

        foreach (var user in users)
        {
            await broadcaster.BroadcastConversationUpdatedAsync(user.Id, updated);
        }

        var response = new ToggleAdminStatusResponse(targetMember.UserId, targetMember.IsAdmin);

        return Result.Success(response);
    }
}