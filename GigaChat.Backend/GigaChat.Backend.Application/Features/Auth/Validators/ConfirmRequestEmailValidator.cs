using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(req => req.Token)
            .NotEmpty();
    }
}