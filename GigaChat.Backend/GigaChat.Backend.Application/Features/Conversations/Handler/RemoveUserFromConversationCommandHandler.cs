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

public class RemoveUserFromConversationCommandHandler(IConversationRepository conversationRepository, IConversationMemberRepository memberRepository, IConversationBroadcaster broadcaster, IUserRepository userRepository) : IRequestHandler<RemoveUserFromConversationCommand, Result>
{
    public async Task<Result> Handle(RemoveUserFromConversationCommand request, CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.FindByIdAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result.Failure(ConversationErrors.NotFound);
        
        if (!conversation.IsGroup)
            return Result.Failure(ConversationErrors.CannotRemoveFromPrivateChat);

        var requester = await memberRepository.FindAsync(request.RequesterId, request.ConversationId, cancellationToken);
        if (requester is null || !requester.IsAdmin)
            return Result.Failure(ConversationErrors.AccessDenied);

        var target = await memberRepository.FindAsync(request.TargetUserId, request.ConversationId, cancellationToken);
        if (target is null)
            return Result.Failure(ConversationErrors.TargetNotMember);

        await memberRepository.RemoveAsync(target, cancellationToken);

        var remainingMembers = await memberRepository.GetMembersAsync(request.ConversationId, cancellationToken);
        var users = new List<IApplicationUser>();

        foreach (var m in remainingMembers)
        {
            var user = await userRepository.FindByIdAsync(m.UserId);
            if (user != null) users.Add(user);
        }

        var response = new ConversationResponse(
            conversation.Id,
            conversation.Name ?? "",
            conversation.IsGroup,
            conversation.AdminId,
            users.Select(u => new ReceiverModel(u.Id, u.UserName, u.Email, u.FirstName, u.LastName, 
                    remainingMembers.Any(m => m.UserId == u.Id && m.IsAdmin)))
                .ToList()
        );

        await Task.WhenAll(users.Select(user =>
            broadcaster.BroadcastConversationUpdatedAsync(user.Id, response)));

        return Result.Success();
    }
}