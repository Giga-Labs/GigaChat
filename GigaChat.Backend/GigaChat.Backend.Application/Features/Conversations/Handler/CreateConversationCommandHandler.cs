using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Models;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Hubs;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Domain.Interfaces.Identity;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class CreateConversationCommandHandler(IUserRepository userRepository, IConversationRepository conversationRepository, IConversationMemberRepository conversationMemberRepository, IConversationInviteLogRepository conversationInviteLogRepository, IBlockedUserRepository blockedUserRepository, IConversationBroadcaster conversationBroadcaster) : IRequestHandler<CreateConversationCommand, Result<ConversationResponse>>
{
    public async Task<Result<ConversationResponse>> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var uniqueIdentifiers = request.MembersList
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var requester = await userRepository.FindByIdAsync(request.RequesterId);
        if (requester is null)
            return Result.Failure<ConversationResponse>(UserErrors.UserNotFound);

        var memberUsers = new List<IApplicationUser>();

        foreach (var identifier in uniqueIdentifiers)
        {
            IApplicationUser? user = identifier.Contains('@')
                ? await userRepository.FindByEmailAsync(identifier)
                : await userRepository.FindByUserNameAsync(identifier);

            if (user is null)
                return Result.Failure<ConversationResponse>(UserErrors.UserNotFound);

            if (user.Id == request.RequesterId)
                continue;

            var hasBlockedRequester = await blockedUserRepository.ExistsAsync(user.Id, request.RequesterId, cancellationToken);
            var hasBlockedTarget = await blockedUserRepository.ExistsAsync(request.RequesterId, user.Id, cancellationToken);
            if (hasBlockedRequester || hasBlockedTarget)
                continue;

            if (uniqueIdentifiers.Count > 1 && !user.AllowGroupInvites)
                continue;

            memberUsers.Add(user);
        }

        if (memberUsers.Count == 0)
            return Result.Failure<ConversationResponse>(ConversationErrors.EmptyParticipantList);

        var isGroup = memberUsers.Count > 1;

        if (!isGroup)
        {
            var hasBlockedExisting = await blockedUserRepository.ExistsAsync(request.RequesterId, memberUsers[0].Id, cancellationToken);
            var hasBlockedRequesterBack = await blockedUserRepository.ExistsAsync(memberUsers[0].Id, request.RequesterId, cancellationToken);

            if (hasBlockedExisting || hasBlockedRequesterBack)
                return Result.Failure<ConversationResponse>(ConversationErrors.BlockedUser);
            
            var existing = await conversationRepository.FindOneToOneAsync(request.RequesterId, memberUsers[0].Id, cancellationToken);
            if (existing is not null)
            {
                var memberIds = await conversationMemberRepository.GetMembersAsync(existing.Id, cancellationToken);
                var users = new List<IApplicationUser>();

                foreach (var id in memberIds.Select(m => m.UserId))
                {
                    var u = await userRepository.FindByIdAsync(id);
                    if (u != null) users.Add(u);
                }

                var existingAdminMap = memberIds.ToDictionary(m => m.UserId, m => m.IsAdmin);
                var oneToOneResponse = new ConversationResponse(
                    existing.Id,
                    existing.Name ?? "",
                    existing.IsGroup,
                    existing.AdminId,
                    users.Select(u => new ReceiverModel(
                        u.Id,
                        u.UserName,
                        u.Email,
                        u.FirstName,
                        u.LastName,
                        existingAdminMap.TryGetValue(u.Id, out var isAdmin) && isAdmin)).ToList()
                );

                return Result.Success(oneToOneResponse);
            }
        }

        var name = isGroup
            ? (request.Name ?? $"Group Chat with {memberUsers.Count + 1} people")
            : $"{memberUsers.First().FirstName} {memberUsers.First().LastName}";

        var conversation = new Conversation
        {
            Id = Guid.NewGuid(),
            Name = name,
            IsGroup = isGroup,
            AdminId = isGroup ? request.RequesterId : null,
            CreatedById = request.RequesterId
        };

        await conversationRepository.AddAsync(conversation, cancellationToken);

        var members = new List<ConversationMember>
        {
            new()
            {
                ConversationId = conversation.Id,
                UserId = request.RequesterId,
                IsAdmin = isGroup,
                JoinedAt = DateTime.UtcNow
            }
        };

        var invites = new List<ConversationInviteLog>();

        foreach (var user in memberUsers)
        {
            members.Add(new ConversationMember
            {
                ConversationId = conversation.Id,
                UserId = user.Id,
                JoinedAt = DateTime.UtcNow,
                InvitedById = request.RequesterId
            });

            invites.Add(new ConversationInviteLog
            {
                Id = Guid.NewGuid(),
                ConversationId = conversation.Id,
                InviteeId = user.Id,
                InviterId = request.RequesterId,
                CreatedAt = DateTime.UtcNow,
                CreatedById = request.RequesterId
            });
        }

        await conversationMemberRepository.AddRangeAsync(members, cancellationToken);
        await conversationInviteLogRepository.AddRangeAsync(invites, cancellationToken);

        var allMembers = new List<IApplicationUser> { requester };
        allMembers.AddRange(memberUsers);

        var adminMap = members.ToDictionary(m => m.UserId, m => m.IsAdmin);

        var response = new ConversationResponse(
            conversation.Id,
            conversation.Name ?? "",
            conversation.IsGroup,
            conversation.AdminId,
            allMembers
                .Select(u => new ReceiverModel(
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    adminMap.TryGetValue(u.Id, out var isAdmin) && isAdmin))
                .ToList()
        );

        await Task.WhenAll(allMembers.Select(m =>
            conversationBroadcaster.BroadcastNewConversationAsync(m.Id, response)));

        return Result.Success(response);
    }
}