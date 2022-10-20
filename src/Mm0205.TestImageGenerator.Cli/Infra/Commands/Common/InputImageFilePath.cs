using System.IO.Abstractions;
using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Models;
using SkiaSharp;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public class InputImageFilePath : IInputImageFilePath
{
    public string Value { get; }

    public InputImageFilePath(string value)
    {
        Value = value;
    }

    public static Result<IInputImageFilePath?> Create(IFileSystem fileSystem, string? imagePath)
    {
        if (imagePath is null)
        {
            return Result.Ok<IInputImageFilePath?>(null);
        }

        return InputFilePath.Create(fileSystem, imagePath) switch
        {
            var ret when ret.IsSuccess => EnsureImageFile(fileSystem, ret.Value.Value),
            var ret => Result.Fail<IInputImageFilePath?>(ret.Errors)
        };
    }

    private static Result<IInputImageFilePath?> EnsureImageFile(IFileSystem fileSystem, string filePath)
    {
        try
        {
            var bytes = fileSystem.File.ReadAllBytes(filePath);
            using var bmp = SKBitmap.Decode(bytes);
            return Result.Ok<IInputImageFilePath?>(new InputImageFilePath(filePath));
        }
        catch (Exception ex)
        {
            return Result.Fail<IInputImageFilePath?>(new Error("入力画像ファイルを画像として読み込めません")
                .WithMetadata(nameof(filePath), filePath)
                .CausedBy(ex)
            );
        }
    }
}