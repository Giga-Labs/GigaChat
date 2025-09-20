using System.Text.RegularExpressions;
using FluentValidation;
using GigaChat.Backend.Application.Features.Conversations.Contracts;

namespace GigaChat.Backend.Application.Features.Conversations.Validators;

public class AddConversationMemberRequestValidator : AbstractValidator<AddConversationMemberRequest>
{
    public AddConversationMemberRequestValidator()
    {
        RuleFor(x => x.MembersList)
            .NotNull().WithMessage("You must include at least one user.")
            .Must(list => list.Count > 0)
            .WithMessage("You must specify at least one recipient.")
            .Must(list => list.All(IsEmailOrUsername))
            .WithMessage("Each member must be a valid email or username.");
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
        return Regex.IsMatch(value, @"^[a-zA-Z0-9._]{3,32}$");
    }
}