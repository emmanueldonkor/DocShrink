using Core.Entities;

namespace Core.Interfaces;

public interface IFileProcessingService
{
    Task<FileProcessingResult> ProcessFileAsync(string fileType, string fileName, Stream fileStream);
}