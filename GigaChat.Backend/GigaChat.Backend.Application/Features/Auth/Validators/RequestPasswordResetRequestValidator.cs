using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class RequestPasswordResetRequestValidator : AbstractValidator<RequestPasswordResetRequest>
{
    public RequestPasswordResetRequestValidator()
    {
        RuleFor(req => req.Email)
            .EmailAddress()
            .NotEmpty();
    }
}