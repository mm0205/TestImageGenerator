using System.IO.Abstractions;
using FluentResults;
using Mm0205.TestImageGenerator.Cli.App.Intentions;
using Mm0205.TestImageGenerator.Cli.Domain.Models;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Copies;

/// <summary>
/// コピーコマンド。
/// </summary>
public class CopyCommandHandler
{
    private readonly IFileSystem _fileSystem;
    private readonly ICopyFileIntentionHandler _handler;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="fileSystem">ファイルシステム。</param>
    /// <param name="handler">インテンションハンドラー。</param>
    public CopyCommandHandler(
        IFileSystem fileSystem,
        ICopyFileIntentionHandler handler
    )
    {
        _fileSystem = fileSystem;
        _handler = handler;
    }

    /// <summary>
    /// コマンドを実行する。
    /// </summary>
    public async Task ExecuteAsync(CopyCommandArguments arguments)
    {
        var intention = CreateIntention(arguments);
        if (intention.IsFailed)
        {
            await ShowErrorsAsync(intention.Errors);
            return;
        }

        await _handler.ExecuteAsync(intention.Value);
    }

    private Result<CopyFileIntention> CreateIntention(
        CopyCommandArguments arguments
    )
    {
        var sourceFilePath = InputFilePath.Create(_fileSystem, arguments.SourceFilePath);
        if (sourceFilePath.IsFailed)
        {
            return Result.Fail<CopyFileIntention>(sourceFilePath.Errors);
        }

        var destinationFolderPath = OutputFolderPath.Create(_fileSystem, arguments.DestinationFolderPath);
        if (destinationFolderPath.IsFailed)
        {
            return Result.Fail<CopyFileIntention>(destinationFolderPath.Errors);
        }

        var outputCount = OutputFileCount.Create(arguments.Count);
        if (outputCount.IsFailed)
        {
            return Result.Fail<CopyFileIntention>(outputCount.Errors);
        }

        var startIndex = OutputIndex.Create(arguments.StartIndex);
        if (outputCount.IsFailed)
        {
            return Result.Fail<CopyFileIntention>(outputCount.Errors);
        }

        return new CopyFileIntention(
            sourceFilePath.Value,
            destinationFolderPath.Value,
            outputCount.Value,
            startIndex.Value
        );
    }

    private static async Task ShowErrorsAsync(List<IError> errors)
    {
        foreach (var e in errors)
        {
            await Console.Error.WriteLineAsync(e.ToString());
        }
    }
}