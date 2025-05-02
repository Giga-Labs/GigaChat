using GigaChat.Backend.Domain.Abstractions;

namespace GigaChat.Backend.Application.Errors;

public static class TokenErrors
{
    public static readonly Error InvalidToken =
        new("Token.Invalid", "The token provided is invalid or malformed.");
}