using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(req => req.Token)
            .NotEmpty();

        RuleFor(req => req.RefreshToken)
            .NotEmpty();
    }
}