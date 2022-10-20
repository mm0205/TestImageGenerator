using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;

namespace Mm0205.TestImageGenerator.Cli.Domain.Models;

/// <summary>
/// 出力用JPEGファイル。
/// </summary>
public class OutputJpeg
{
    /// <summary>
    /// ファイルパス。
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// ファイル名拡張子無し。
    /// </summary>
    public string BaseName { get; }

    public OutputJpeg(string filePath, string baseName)
    {
        FilePath = filePath;
        BaseName = baseName;
    }

    public static Result<OutputJpeg> Create(
        IFileNameService fileNameService,
        IOutputFolderPath folderPath,
        OutputIndex index
    )
    {
        var baseName = $"{index.Value:D5}";
        var filePath = fileNameService.Combine(folderPath, $"{baseName}.jpg");

        return new OutputJpeg(filePath, baseName);
    }
}