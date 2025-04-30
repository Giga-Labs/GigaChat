namespace GigaChat.Backend.Domain.Abstractions.Consts;

public static class RegexPatterns
{
    public const string Password = "^(?=.*[0-9])(?=.*[\\W])(?=.*[a-z])(?=.*[A-Z]).{8,}$";
}