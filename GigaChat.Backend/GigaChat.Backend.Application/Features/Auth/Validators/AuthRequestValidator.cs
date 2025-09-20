using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class AuthRequestValidator : AbstractValidator<AuthRequest>
{
    public AuthRequestValidator()
    {
        RuleFor(req => req.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(req => req.Password)
            .NotEmpty();
    }
}