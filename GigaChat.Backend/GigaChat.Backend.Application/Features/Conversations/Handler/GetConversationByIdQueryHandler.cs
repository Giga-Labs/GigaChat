using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Features.Conversations.Queries;
using GigaChat.Backend.Application.Models;
using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Interfaces.Identity;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Handler;

public class GetConversationByIdQueryHandler(IConversationRepository conversationRepository, IConversationMemberRepository conversationMemberRepository, IUserRepository userRepository) : IRequestHandler<GetConversationByIdQuery, Result<ConversationResponse>>
{
    public async Task<Result<ConversationResponse>> Handle(GetConversationByIdQuery request, CancellationToken cancellationToken)
    {
        var conversation = await conversationRepository.FindByIdAsync(request.ConversationId, cancellationToken);
        if (conversation is null)
            return Result.Failure<ConversationResponse>(ConversationErrors.NotFound);

        var isMember = await conversationMemberRepository.IsMemberAsync(request.RequesterId, request.ConversationId, cancellationToken);
        if (!isMember)
            return Result.Failure<ConversationResponse>(ConversationErrors.AccessDenied);

        var memberIds = await conversationMemberRepository.GetMembersAsync(request.ConversationId, cancellationToken);
        var members = new List<IApplicationUser>();

        foreach (var member in memberIds)
        {
            var user = await userRepository.FindByIdAsync(member.UserId);
            if (user is not null)
                members.Add(user);
        }

        var response = new ConversationResponse(
            conversation.Id,
            conversation.Name ?? "",
            conversation.IsGroup,
            conversation.AdminId,
            members.Select(u => new ReceiverModel(u.Id, u.UserName, u.Email, u.FirstName, u.LastName)).ToList()
        );

        return Result.Success(response);
    }
}