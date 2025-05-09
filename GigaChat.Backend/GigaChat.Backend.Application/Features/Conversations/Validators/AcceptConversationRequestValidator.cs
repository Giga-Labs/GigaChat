using FluentValidation;
using GigaChat.Backend.Application.Features.Conversations.Contracts;

namespace GigaChat.Backend.Application.Features.Conversations.Validators;

public class AcceptConversationRequestValidator : AbstractValidator<AcceptConversationRequest>
{
    public AcceptConversationRequestValidator()
    {
        RuleFor(req => req.Accept)
            .NotNull();
    }
}