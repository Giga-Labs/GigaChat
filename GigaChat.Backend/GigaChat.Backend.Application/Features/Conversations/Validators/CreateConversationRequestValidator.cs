using System.Text.RegularExpressions;
using FluentValidation;
using GigaChat.Backend.Application.Features.Conversations.Contracts;

namespace GigaChat.Backend.Application.Features.Conversations.Validators;

public class CreateConversationRequestValidator : AbstractValidator<CreateConversationRequest>
{
    public CreateConversationRequestValidator()
    {
        RuleFor(x => x.MembersList)
            .NotNull().WithMessage("You must include at least one user.")
            .Must(list => list.Count > 0)
            .WithMessage("You must specify at least one recipient.")
            .Must(list => list.All(IsEmailOrUsername))
            .WithMessage("Each member must be a valid email or username.");

        RuleFor(x => x.Name)
            .MaximumLength(100)
            .WithMessage("Conversation name can't be more than 100 characters.");
    }

    private bool IsEmailOrUsername(string value)
    {
        return IsEmail(value) || IsUsername(value);
    }

    private bool IsEmail(string value)
    {
        return Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private bool IsUsername(string value)
    {
        // alphanumeric, dot, underscore, 3-32 chars
        return Regex.IsMatch(value, @"^[a-zA-Z0-9._]{3,32}$");
    }
}