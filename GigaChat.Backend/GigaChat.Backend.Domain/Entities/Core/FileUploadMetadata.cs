namespace GigaChat.Backend.Domain.Entities.Core;

public class FileUploadMetadata
{
    public Guid Id { get; set; }
    public string UploadedById { get; set; }
    public string Url { get; set; }
    public string FileName { get; set; }
    public string MimeType { get; set; }
    public long SizeInBytes { get; set; }
    public bool IsVoice { get; set; }
    public DateTime UploadedAt { get; set; }
}