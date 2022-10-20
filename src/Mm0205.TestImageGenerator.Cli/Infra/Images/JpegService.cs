using System.IO.Abstractions;
using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Domain.Models;
using SkiaSharp;

namespace Mm0205.TestImageGenerator.Cli.Infra.Images;

public class JpegService : IJpegService
{
    private readonly IFileSystem _fileSystem;

    public JpegService(IFileSystem fileSystem)
    {
        _fileSystem = fileSystem;
    }

    public Task<Result> CreateAsync(
        OutputJpeg outputJpeg,
        IInputImageFilePath? templateFilePath
    )
    {
        return Task.Run(() =>
        {
            using var template = LoadTemplate(templateFilePath);

            using var bmp = new SKBitmap(template.Width, template.Height);
            using var canvas = new SKCanvas(bmp);
            using (var paint = new SKPaint())
            {
                canvas.DrawBitmap(template, 0, 0, paint);
            }

            using (var paint = new SKPaint())
            {
                paint.TextSize = 512;
                paint.Color = SKColors.Red;

                var bounds = new SKRect();
                paint.MeasureText(outputJpeg.BaseName, ref bounds);

                var w = (bmp.Width - bounds.Width) / 2;
                var h = (bmp.Height - bounds.Height) / 2;

                canvas.DrawText(outputJpeg.BaseName, w, h, paint);
            }

            var jpegData = bmp.Encode(SKEncodedImageFormat.Jpeg, 100);
            return Result.Try(new Func<Task>(async () =>
                {
                    await File.WriteAllBytesAsync(outputJpeg.FilePath, jpegData.ToArray());
                }),
                exception => new Error("jpeg save error").CausedBy(exception));
        });
    }

    private SKBitmap LoadTemplate(IInputImageFilePath? templateFilePath)
    {
        if (templateFilePath == null)
        {
            var template = new SKBitmap(1920, 1080, SKColorType.Rgb888x, SKAlphaType.Opaque);
            using var canvas = new SKCanvas(template);
            using var paint = new SKPaint();
            canvas.Clear(SKColors.White);
            return template;
        }

        var bytes = _fileSystem.File.ReadAllBytes(templateFilePath.Value);
        return SKBitmap.Decode(bytes);
    }
}