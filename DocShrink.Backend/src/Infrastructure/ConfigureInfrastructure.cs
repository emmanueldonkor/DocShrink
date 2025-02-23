using Core.Interfaces;
using Infrastructure.Compression;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureInfrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBlobStorageService, BlobStorageService>();

        services.AddSingleton<ICompressor, DocxCompressor>();
        services.AddSingleton<ICompressor, PdfCompressor>();
        services.AddSingleton<ICompressor, XlsxCompressor>();
        services.AddSingleton<ICompressionFactory, CompressionFactory>();

        return services;
    }
}