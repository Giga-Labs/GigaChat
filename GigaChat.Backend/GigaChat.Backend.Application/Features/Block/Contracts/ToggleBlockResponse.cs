namespace GigaChat.Backend.Application.Features.Block.Contracts;

public record ToggleBlockResponse(string UserId, bool IsBlocked);