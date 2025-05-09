using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Features.Conversations.Queries;
using GigaChat.Backend.Application.Models;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Interfaces.Identity;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class GetAccessibleConversationsQueryHandler(IConversationRepository conversationRepository, IConversationMemberRepository conversationMemberRepository, IUserRepository userRepository) : IRequestHandler<GetAccessibleConversationsQuery, Result<IReadOnlyList<ConversationResponse>>>
{
    public async Task<Result<IReadOnlyList<ConversationResponse>>> Handle(GetAccessibleConversationsQuery request, CancellationToken cancellationToken)
    {
        var conversationIds = await conversationMemberRepository
            .GetConversationIdsForUserAsync(request.RequesterId, cancellationToken);

        var conversations = new List<ConversationResponse>();

        foreach (var conversationId in conversationIds)
        {
            var conversation = await conversationRepository.FindByIdAsync(conversationId, cancellationToken);
            if (conversation == null)
                continue;

            var members = await conversationMemberRepository.GetMembersAsync(conversationId, cancellationToken);
            var userModels = new List<IApplicationUser>();

            foreach (var member in members)
            {
                var user = await userRepository.FindByIdAsync(member.UserId);
                if (user != null)
                    userModels.Add(user);
            }

            var convoResponse = new ConversationResponse(
                conversation.Id,
                conversation.Name ?? "",
                conversation.IsGroup,
                conversation.AdminId,
                userModels.Select(u =>
                    new ReceiverModel(u.Id, u.UserName, u.Email, u.FirstName, u.LastName)
                ).ToList()
            );

            conversations.Add(convoResponse);
        }

        return Result.Success<IReadOnlyList<ConversationResponse>>(conversations);
    }
}