using GigaChat.Backend.Api.Extensions;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GigaChat.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthRequest request, CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new AuthQuery(request.Email, request.Password), cancellationToken);
            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(
                    new RegisterCommand(request.UserName, request.Email, request.Password, request.FirstName,
                        request.LastName), cancellationToken);
            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var result = await mediator.Send(new RefreshTokenCommand(request.Token, request.RefreshToken), cancellationToken);
            return result.Succeeded ? Ok(result.Value) : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            var result = await mediator.Send(new ConfirmEmailCommand(request.Token));
            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("resend-confirmation-email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailRequest request)
        {
            var result = await mediator.Send(new ResendConfirmationEmailCommand(request.Email));
            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request)
        {
            var result = await mediator.Send(new SendResetPasswordCodeCommand(request.Email));
            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var result = await mediator.Send(new VerifyOtpCommand(request.Otp,request.Email));
            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var result = await mediator.Send(new ResetPasswordCommand(request.Email,request.Otp,request.NewPassword));
            return result.Succeeded ? Ok() : result.ToProblem(result.Error.ToStatusCode());
        }
    }
}
