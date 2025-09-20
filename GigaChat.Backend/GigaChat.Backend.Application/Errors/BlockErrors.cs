using GigaChat.Backend.Domain.Abstractions;

namespace GigaChat.Backend.Application.Errors;

public static class BlockErrors
{
    public static readonly Error SelfBlock = new("Block.SelfBlock", "You cannot block yourself. Try inner peace, not inner war.");
}