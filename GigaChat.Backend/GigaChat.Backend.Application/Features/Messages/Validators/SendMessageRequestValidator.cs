using FluentValidation;
using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Domain.Enums.Core;

namespace GigaChat.Backend.Application.Features.Messages.Validators;

public class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(req => req.Content)
            .NotEmpty()
            .MaximumLength(5000);
    }
}