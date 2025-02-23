using Core.Interfaces;

namespace Infrastructure.Compression;

public class CompressionFactory : ICompressionFactory
{
    private readonly IDictionary<string, ICompressor> compressionHandlers;

    public CompressionFactory(IEnumerable<ICompressor> compressors)
    {
        compressionHandlers = compressors.ToDictionary(c => c.FileType, StringComparer.OrdinalIgnoreCase);
    }

    public ICompressor GetHandler(string fileType)
    {
        if (compressionHandlers.TryGetValue(fileType, out var handler))
            return handler;

        throw new InvalidOperationException($"No compression handler available for file type '{fileType}'.");
    }
}