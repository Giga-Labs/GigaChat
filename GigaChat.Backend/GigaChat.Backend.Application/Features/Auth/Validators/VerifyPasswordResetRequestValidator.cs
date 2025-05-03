using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class VerifyPasswordResetRequestValidator : AbstractValidator<VerifyPasswordResetOtpRequest>
{
    public VerifyPasswordResetRequestValidator()
    {
        RuleFor(req => req.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(req => req.OtpCode)
            .NotEmpty()
            .Length(6);
    }
}