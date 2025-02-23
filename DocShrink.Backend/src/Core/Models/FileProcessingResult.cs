namespace Core.Entities;

public record class FileProcessingResult
{
    public required string FileName { get; set; }
    public long OriginalSize { get; set; }
    public long CompressedSize { get; set; }
    public required string DownloadUrl   { get; set; }
}