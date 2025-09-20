namespace GigaChat.Backend.Application.Features.Block.Contracts;

public record BlockedUserResponse
(
    string Id,
    string UserName,
    string Email,
    DateTime BlockedAt
);