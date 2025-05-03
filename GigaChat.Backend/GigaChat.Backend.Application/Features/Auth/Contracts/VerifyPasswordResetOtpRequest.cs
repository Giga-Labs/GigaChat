namespace GigaChat.Backend.Application.Features.Auth.Contracts;

public record VerifyPasswordResetOtpRequest
(
    string Email,
    string OtpCode
);