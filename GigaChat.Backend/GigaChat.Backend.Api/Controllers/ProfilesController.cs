using System.Security.Claims;
using GigaChat.Backend.Api.Extensions;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Profile.Commands;
using GigaChat.Backend.Application.Features.Profile.Contracts;
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
    public class ProfilesController(IMediator mediator) : ControllerBase
    {
        [HttpPut("")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request,
            CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var command = new UpdateProfileCommand(
                requesterId,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Username,
                request.AllowGroupInvites,
                request.TwoFactorEnabled,
                request.ProfilePicture,
                request.ProfilePictureUrl
            );
            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? NoContent() : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(TokenErrors.InvalidToken.ToStatusCode());

            var result = await mediator.Send(new GetProfileQuery(requesterId), cancellationToken);
            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(requesterId))
                return Result.Failure(TokenErrors.InvalidToken).ToProblem(StatusCodes.Status401Unauthorized);

            var command = new ChangePasswordCommand(
                RequesterId: requesterId,
                CurrentPassword: request.CurrentPassword,
                NewPassword: request.NewPassword
            );

            var result = await mediator.Send(command, cancellationToken);

            return result.Succeeded ? NoContent() : result.ToProblem(result.Error.ToStatusCode());
        }
    }
}
