using FluentValidation;
using GigaChat.Backend.Application.Features.Profile.Contracts;

namespace GigaChat.Backend.Application.Features.Profile.Validators;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(30)
            .Matches("^[a-zA-Z0-9._-]+$")
            .WithMessage("Username can only contain letters, numbers, dots, underscores, and hyphens.");

        RuleFor(x => x.ProfilePicture)
            .Must(file => file == null || file.Length < 5 * 1024 * 1024)
            .WithMessage("Profile picture must be less than 5MB.")
            .Must(file => file == null || new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("Only JPG and PNG formats are supported.");

        RuleFor(x => x.ProfilePictureUrl)
            .Must(url => string.IsNullOrWhiteSpace(url) || Uri.TryCreate(url, UriKind.Absolute, out var result))
            .WithMessage("Profile picture URL must be a valid link.")
            .When(x => x.ProfilePicture == null); // only validate if no file is uploaded
    }
}