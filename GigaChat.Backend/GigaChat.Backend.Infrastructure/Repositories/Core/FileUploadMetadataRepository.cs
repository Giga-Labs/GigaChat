using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class FileUploadMetadataRepository(CoreDbContext coreDbContext) : IFileUploadMetadataRepository
{
    public async Task AddAsync(FileUploadMetadata metadata, CancellationToken cancellationToken = default)
    {
        await coreDbContext.FileUploadMetadatas.AddAsync(metadata, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<FileUploadMetadata?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.FileUploadMetadatas
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<FileUploadMetadata>> GetFilesUploadedByUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.FileUploadMetadatas
            .Where(f => f.UploadedById == userId)
            .OrderByDescending(f => f.UploadedAt)
            .ToListAsync(cancellationToken);
    }
}