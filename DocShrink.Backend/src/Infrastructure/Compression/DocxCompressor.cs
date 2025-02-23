using Application.Exceptions;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Saving;
using Core.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Infrastructure.Compression
{
    public class DocxCompressor : ICompressor
    {
        public string FileType => "docx";

        public async Task<Stream> CompressAsync(Stream fileStream)
        {
            if (fileStream is null || !fileStream.CanRead)
            {
                throw new InvalidFileException("The input file is null or unreadable");
            }
            var outputStream = new MemoryStream();

            try
            {
                var doc = new Document(fileStream);
                doc.Cleanup();

                var imageTasks = doc.GetChildNodes(NodeType.Shape, true)
                    .Cast<Shape>()
                    .Where(shape => shape.IsImage)
                    .Select(async shape =>
                    {
                        using var imageStream = shape.ImageData.ToStream();
                        using var image = await Task.Run(() => Image.Load(imageStream));

                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(image.Width / 2, image.Height / 2)
                        }));

                        using var compressedImageStream = new MemoryStream();
                        var jpegEncoder = new JpegEncoder { Quality = 75 };
                        await Task.Run(() => image.Save(compressedImageStream, jpegEncoder));

                        compressedImageStream.Position = 0;
                        shape.ImageData.SetImage(compressedImageStream);
                    });

                await Task.WhenAll(imageTasks);

                var saveOptions = new OoxmlSaveOptions
                {
                    CompressionLevel = CompressionLevel.Maximum
                };

                await Task.Run(() => doc.Save(outputStream, saveOptions));
                outputStream.Position = 0;
            }
            catch (Exception ex)
            {
                throw new CompressionFailureException("Error during document compression.", ex);
            }

            return outputStream;
        }
    }
}



