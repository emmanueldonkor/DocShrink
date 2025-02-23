namespace Core.Interfaces;

public interface ICompressionFactory
{
    ICompressor GetHandler(string fileType);
}