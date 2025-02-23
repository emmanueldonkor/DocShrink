namespace Core.Interfaces;

public interface ICompressor
{
    string FileType { get; }
    Task<Stream> CompressAsync(Stream fileStream);
}