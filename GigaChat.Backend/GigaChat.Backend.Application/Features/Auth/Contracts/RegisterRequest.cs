namespace GigaChat.Backend.Application.Features.Auth.Contracts;

public record RegisterRequest
(
    string UserName,
    string Email,
    string Password,
    string FirstName,
    string LastName    
);