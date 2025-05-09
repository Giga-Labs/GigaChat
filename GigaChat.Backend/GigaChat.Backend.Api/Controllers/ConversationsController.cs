using System.Security.Claims;
using GigaChat.Backend.Api.Extensions;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Conversations.Commands;
using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Features.Conversations.Queries;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new CreateConversationCommand(userId, request.Name, request.MembersList);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? CreatedAtAction(nameof(GetById), new { conversationId = result.Value.Id }, result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpPost("{conversationId}/accept")]
        public async Task<IActionResult> AcceptAsync([FromRoute] Guid conversationId,
            [FromBody] AcceptConversationRequest request, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new AcceptConversationCommand(userId, conversationId, request.Accept);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpGet("{conversationId}")]
        public async Task<IActionResult> GetById([FromRoute] Guid conversationId,
            CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var query = new GetConversationByIdQuery(userId, conversationId);
            var result = await mediator.Send(query, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var query = new GetAccessibleConversationsQuery(userId);
            var result = await mediator.Send(query, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
    }
}
