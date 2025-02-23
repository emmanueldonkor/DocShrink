
using Core.Entities;
using Core.Interfaces;

namespace Application.Services;

public class FileProcessingService : IFileProcessingService
{
    private readonly ICompressionFactory compressionFactory;
    private readonly IBlobStorageService blobStorageService;
    private readonly ISanitizationService sanitizationService;

    public FileProcessingService(
        ICompressionFactory compressionFactory,
        IBlobStorageService blobStorageService,
        ISanitizationService sanitizationService
        )
    {
        this.compressionFactory = compressionFactory;
        this.blobStorageService = blobStorageService;
        this.sanitizationService = sanitizationService;
    }

    public async Task<FileProcessingResult> ProcessFileAsync(string fileType, string fileName, Stream fileStream)
    {
        fileName = sanitizationService.SanitizeInput(fileName);

        var compressedStream = await CompressFileAsync(fileType, fileStream);

        var blobUrl = await UploadFileAsync(compressedStream, fileName);

        var (originalSize, compressedSize) = GetFileSizes(fileStream, compressedStream);

        return CreateFileProcessingResult(fileName, originalSize, compressedSize, blobUrl);
    }

    private async Task<Stream> CompressFileAsync(string fileType, Stream fileStream)
    {
        var compressor = compressionFactory.GetHandler(fileType);
        return await compressor.CompressAsync(fileStream);
    }

    private async Task<string> UploadFileAsync(Stream compressedStream, string fileName)
    {
        return await blobStorageService.UploadFileAsync(compressedStream, fileName, "application/octet-stream");
    }

    private static (long originalSize, long compressedSize) GetFileSizes(Stream fileStream, Stream compressedStream)
    {
        return (fileStream.Length, compressedStream.Length);
    }

    private static FileProcessingResult CreateFileProcessingResult(
        string fileName, long originalSize, long compressedSize, string blobUrl)
    {
        return new FileProcessingResult
        {
            FileName = fileName,
            OriginalSize = originalSize,
            CompressedSize = compressedSize,
            DownloadUrl = blobUrl
        };
    }
}
