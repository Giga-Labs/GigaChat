namespace GigaChat.Backend.Application.Features.Auth.Contracts;

public record VerifyOtpRequest
(
    string Otp,
    string Email
);