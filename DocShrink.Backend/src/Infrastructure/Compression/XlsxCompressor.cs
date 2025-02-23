using Application.Exceptions;
using Aspose.Cells;
using Core.Interfaces;

namespace Infrastructure.Compression;

public class XlsxCompressor : ICompressor
{
    public string FileType => "xlsx";

    public async Task<Stream> CompressAsync(Stream fileStream)
    {
        if (fileStream == null || !fileStream.CanRead)
            throw new InvalidFileException("The input file stream is null or unreadable.");

        var outputStream = new MemoryStream();

        try
        { 
            var workbook = new Workbook(fileStream);

            var saveOptions = new XlsbSaveOptions
            {
                CompressionType = OoxmlCompressionType.Level9 
            };

          await Task.Run(() => workbook.Save(outputStream, saveOptions));

            outputStream.Position = 0; 
        }
        catch (Exception ex)
        {
            throw new CompressionFailureException("Error occurred during Excel compression.", ex);
        }

        return outputStream;
    }
}
