using FluentValidation;
using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Domain.Abstractions.Consts;

namespace GigaChat.Backend.Application.Features.Auth.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(req => req.UserName)
            .NotEmpty()
            .Length(3, 32);
        
        RuleFor(req => req.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(req => req.Password)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
        
        RuleFor(req => req.FirstName)
            .NotEmpty()
            .Length(3, 100);

        RuleFor(req => req.LastName)
            .NotEmpty()
            .Length(3, 100);
    }
}