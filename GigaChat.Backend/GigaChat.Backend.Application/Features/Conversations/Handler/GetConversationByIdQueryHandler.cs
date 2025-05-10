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

        var memberEntities = await conversationMemberRepository.GetMembersAsync(request.ConversationId, cancellationToken);
        
        var responses = new List<ReceiverModel>();

        foreach (var member in memberEntities)
        {
            var user = await userRepository.FindByIdAsync(member.UserId);
            if (user is null) continue;

            responses.Add(new ReceiverModel(
                user.Id,
                user.UserName,
                user.Email,
                user.FirstName,
                user.LastName,
                member.IsAdmin
            ));
        }

        var response = new ConversationResponse(
            conversation.Id,
            conversation.Name ?? "",
            conversation.IsGroup,
            conversation.AdminId,
            responses
        );

        return Result.Success(response);
    }
}