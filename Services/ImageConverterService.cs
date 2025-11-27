using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;

namespace tinyutils.Services;

public class ImageConverterService
{
    public async Task<ImageConversionResult> ConvertImageAsync(
        Stream inputStream,
        string outputFormat
    )
    {
        var result = new ImageConversionResult();

        try
        {
            using var image = await Image.LoadAsync(inputStream);
            using var outputStream = new MemoryStream();

            switch (outputFormat.ToLowerInvariant())
            {
                case "jpeg":
                case "jpg":
                    await image.SaveAsJpegAsync(outputStream, new JpegEncoder
                    {
                        Quality = 90
                    });
                    result.ContentType = "image/jpg";
                    result.FileExtension = "jpg";
                    break;

                case "png":
                    await image.SaveAsPngAsync(outputFormat);
                    result.ContentType = "image/png";
                    result.FileExtension = "png";
                    break;

                case "webp":
                    await image.SaveAsWebpAsync(outputStream, new WebpEncoder
                    {
                        // quality 90 made some files bigger :O
                        Quality = 75,
                        // try lossy
                        FileFormat = WebpFileFormatType.Lossy
                    });
                    result.ContentType = "image/webp";
                    result.FileExtension = "webp";
                    break;

                default:
                    result.ErrorMessage = $"Unsupported format: {outputFormat}";
                    return result;
            }

            result.ImageData = outputStream.ToArray();
            result.Success = true;
            result.Width = image.Width;
            result.Height = image.Height;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
        }

        return result;
    }
}

public class ImageConversionResult
{
    public bool Success { get; set; }
    public byte[]? ImageData { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public int Width { get; set; }
    public int Height { get; set; }
    public string? ErrorMessage { get; set; }
}