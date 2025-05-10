using System.Security.Claims;
using GigaChat.Backend.Api.Extensions;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Messages.Commands;
using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Application.Features.Messages.Queries;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Backend.Api.Controllers
{
    [Route("Conversations/{conversationId}/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController(IMediator mediator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> SendMessage([FromRoute] Guid conversationId, [FromBody] SendMessageRequest request)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new SendMessageCommand(conversationId, requesterId, request.Content);
            var result = await mediator.Send(command);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpGet("")]
        public async Task<IActionResult> GetMessagesAsync([FromRoute] Guid conversationId,
            CancellationToken cancellationToken)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var query = new GetMessagesQuery(requesterId, conversationId);
            var result = await mediator.Send(query, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
    }
}
