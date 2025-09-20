using System.Security.Claims;
using GigaChat.Backend.Api.Extensions;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Features.Conversations.Queries;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ConversationsController(IMediator mediator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateConversationRequest request, CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new CreateConversationCommand(requesterId, request.Name, request.MembersList);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? CreatedAtAction(nameof(GetById), new { conversationId = result.Value.Id }, result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpPost("{conversationId}/accept")]
        public async Task<IActionResult> AcceptAsync([FromRoute] Guid conversationId,
            [FromBody] AcceptConversationRequest request, CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new AcceptConversationCommand(requesterId, conversationId, request.Accept, request.ConnectionId);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid conversationId,
            CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var query = new GetConversationByIdQuery(requesterId, conversationId);
            var result = await mediator.Send(query, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var query = new GetAccessibleConversationsQuery(requesterId);
            var result = await mediator.Send(query, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpDelete("{conversationId}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] Guid conversationId,
            CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new RemoveConversationCommand(requesterId, conversationId);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? NoContent() : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpPut("{conversationId}/members/{userId}/admin")]
        public async Task<IActionResult> ToggleAdminAsync([FromRoute] Guid conversationId, [FromRoute] string userId,
            CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new ToggleAdminStatusCommand(requesterId, conversationId, userId);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpDelete("{conversationId}/members/{userId}")]
        public async Task<IActionResult> RemoveMember([FromRoute] Guid conversationId, [FromRoute] string userId, CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Unauthorized();

            var command = new RemoveUserFromConversationCommand(requesterId, conversationId, userId);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? NoContent() : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpPost("{conversationId}/clear")]
        public async Task<IActionResult> ClearConversationAsync([FromRoute] Guid conversationId, CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Unauthorized();

            var command = new ClearConversationCommand(conversationId, requesterId);
            var result = await mediator.Send(command, cancellationToken);
            
            return result.Succeeded ? NoContent() : result.ToProblem(result.Error.ToStatusCode());
        }
    }
}
