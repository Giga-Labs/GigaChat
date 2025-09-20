using FluentValidation;
using GigaChat.Backend.Application.Features.Profile.Contracts;
using GigaChat.Backend.Domain.Abstractions.Consts;

namespace GigaChat.Backend.Application.Features.Profile.Validators;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage(
                "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

        RuleFor(req => req.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage(
                "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

    }
}