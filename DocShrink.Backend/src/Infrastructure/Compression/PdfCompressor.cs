using Aspose.Pdf;
using Aspose.Pdf.Optimization;
using Core.Interfaces;

namespace Infrastructure.Compression;

public class PdfCompressor : ICompressor
{
    public string FileType => "pdf";

    public async Task<Stream> CompressAsync(Stream fileStream)
    {
        var pdfDocument = new Document(fileStream);

        var optimizationOptions = new OptimizationOptions
        {
            LinkDuplicateStreams = true,
            RemoveUnusedObjects = true,
            RemoveUnusedStreams = true
        };
        optimizationOptions.ImageCompressionOptions.CompressImages = true;
        optimizationOptions.ImageCompressionOptions.ImageQuality = 75;

        pdfDocument.OptimizeResources(optimizationOptions);

        var outputStream = new MemoryStream();
        pdfDocument.Save(outputStream);

        outputStream.Position = 0;

        return await Task.FromResult(outputStream);
    }
}