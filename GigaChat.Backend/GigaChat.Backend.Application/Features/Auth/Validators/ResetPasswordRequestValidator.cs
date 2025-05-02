using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Domain.Abstractions.Consts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(request => request.Otp)
            .NotEmpty();

        RuleFor(request => request.NewPassword)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

    }
}