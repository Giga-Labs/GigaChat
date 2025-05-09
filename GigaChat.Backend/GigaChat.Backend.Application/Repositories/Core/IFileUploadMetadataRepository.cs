using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IFileUploadMetadataRepository
{
    Task AddAsync(FileUploadMetadata metadata, CancellationToken cancellationToken = default);
    Task<FileUploadMetadata?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FileUploadMetadata>> GetFilesUploadedByUserAsync(string userId, CancellationToken cancellationToken = default);
}