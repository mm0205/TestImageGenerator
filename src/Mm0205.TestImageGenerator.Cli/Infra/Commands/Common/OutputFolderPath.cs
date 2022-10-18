using System.IO.Abstractions;
using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Models;
using Mm0205.TestImageGenerator.Cli.Util;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public class OutputFolderPath : IOutputFolderPath
{
    public string FolderPath { get; }

    private OutputFolderPath(string folderPath)
    {
        FolderPath = folderPath;
    }

    public static Result<IOutputFolderPath> Create(IFileSystem fileSystem, string folderPath)
    {
        if (!fileSystem.Path.TryGetFullPath(folderPath, out var fullPath))
        {
            return Result.Fail<IOutputFolderPath>(
                new Error("出力先フォルダの絶対パスを取得できません。")
                    .WithMetadata(nameof(folderPath), folderPath));
        }

        if (!Directory.Exists(fullPath))
        {
            return Result.Fail<IOutputFolderPath>(
                new Error("出力先フォルダが見つかりません。")
                    .WithMetadata(nameof(fullPath), fullPath));
        }

        return new OutputFolderPath(folderPath);
    }
}