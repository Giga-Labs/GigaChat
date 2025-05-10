using GigaChat.Backend.Application.Services.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GigaChat.Backend.Infrastructure.Services.Storage;

public class LocalFileStorageService(IWebHostEnvironment webHostEnvironment) : IFileStorageService
{
    public async Task<string> UploadProfilePictureAsync(string userId, IFormFile file)
    {
        var folder = Path.Combine(webHostEnvironment.WebRootPath ?? "wwwroot", "pfp");
        Directory.CreateDirectory(folder);

        var fileName = $"{userId}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(folder, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return $"/pfp/{fileName}";
    }
}