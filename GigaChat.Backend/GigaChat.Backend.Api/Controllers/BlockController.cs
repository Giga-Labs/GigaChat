using System.Security.Claims;
using GigaChat.Backend.Api.Extensions;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Block.Commands;
using GigaChat.Backend.Application.Features.Block.Queries;
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
    public class BlockController(IMediator mediator) : ControllerBase
    {
        [HttpPost("{userId}/toggle")]
        public async Task<IActionResult> ToggleBlockAsync([FromRoute] string userId,
            CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new ToggleBlockCommand(requesterId, userId);
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllBlockedAsync(CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var query = new GetBlockedUsersQuery(requesterId);
            var result = await mediator.Send(query, cancellationToken);

            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
    }
}
