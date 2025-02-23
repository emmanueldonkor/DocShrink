using Azure.Storage.Blobs;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Storage;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient blobServiceClient;
    private readonly string containerName;
    public BlobStorageService(IConfiguration configuration)
    {
        var connectionString = configuration["AzureStorage:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Azure blob storage connection string is not configured!");
        containerName = configuration["AzureStorage:ContainerName"]!;

        blobServiceClient = new(connectionString);
    }
    public async Task DeleteFileAsync(string fileName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient("shrinked-files");
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync();
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);

        fileStream.Position = 0;
        await blobClient.UploadAsync(fileStream, overwrite: true);

        var stringUri = blobClient.Uri.ToString();
        return stringUri;
    }

    public async Task<Stream> GetFileAsync(string fileName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        if (!await blobClient.ExistsAsync())
        {
            throw new FileNotFoundException($"File '{fileName}' not found in blob storage.");
        }

        var memoryStream = new MemoryStream();
        await blobClient.DownloadToAsync(memoryStream);
        memoryStream.Position = 0; 
        return memoryStream;
    }

}