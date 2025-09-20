namespace GigaChat.Backend.Application.Features.Auth.Contracts;

public record AuthRequest
(
    string Email,
    string Password
);