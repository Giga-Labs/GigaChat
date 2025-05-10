namespace GigaChat.Backend.Application.Features.Profile.Contracts;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);