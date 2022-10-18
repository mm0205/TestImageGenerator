using System.IO.Abstractions;
using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Models;
using Mm0205.TestImageGenerator.Cli.Util;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

internal class InputFilePath : IInputFilePath
{
    public string Value { get; }

    private InputFilePath(string filePath)
    {
        Value = filePath;
    }

    public static Result<IInputFilePath> Create(IFileSystem fileSystem, string filePath)
    {
        if (!fileSystem.Path.TryGetFullPath(filePath, out var fullPath))
        {
            return Result.Fail<IInputFilePath>(
                new Error("入力ファイルの絶対パスを取得できません。")
                    .WithMetadata(nameof(filePath), filePath));
        }

        if (!fileSystem.File.Exists(fullPath))
        {
            return Result.Fail<IInputFilePath>(
                new Error("入力ファイルが見つかりません。")
                    .WithMetadata(nameof(fullPath), fullPath)
            );
        }

        try
        {
            using var _ = fileSystem.File.OpenRead(fullPath)
                          ?? throw new Exception();
        }
        catch (Exception ex)
        {
            return Result.Fail<IInputFilePath>(
                new Error("入力ファイルを読み込み用にオープンできません。")
                    .WithMetadata(nameof(fullPath), fullPath)
                    .CausedBy(ex)
            );
        }

        return new InputFilePath(fullPath);
    }
}