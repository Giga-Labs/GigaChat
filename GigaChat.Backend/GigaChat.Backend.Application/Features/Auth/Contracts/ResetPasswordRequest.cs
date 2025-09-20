namespace GigaChat.Backend.Application.Features.Auth.Contracts;

public record ResetPasswordRequest
(
    string Token,
    string NewPassword
);