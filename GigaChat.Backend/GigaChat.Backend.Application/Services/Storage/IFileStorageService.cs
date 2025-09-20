using Microsoft.AspNetCore.Http;

namespace GigaChat.Backend.Application.Services.Storage;

public interface IFileStorageService
{
    Task<string> UploadProfilePictureAsync(string userId, IFormFile file);
    Task<string> UploadMessageAttachmentAsync(string userId, Guid conversationId, IFormFile file);
}