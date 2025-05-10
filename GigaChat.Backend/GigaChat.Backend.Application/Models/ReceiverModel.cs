namespace GigaChat.Backend.Application.Models;

public record ReceiverModel
(
  string Id,
  string UserName,
  string Email,
  string FirstName,
  string LastName,
  bool IsAdmin,
  string? ProfilePictureUrl
);