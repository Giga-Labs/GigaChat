using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .EmailAddress();
    }
}