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
    private readonly IErrorShower _errorShower;

    /// <summary>
    /// コンストラクタ。
    /// </summary>
    /// <param name="fileSystem">ファイルシステム。</param>
    /// <param name="handler">インテンションハンドラー。</param>
    /// <param name="errorShower">エラー表示。</param>
    public CopyCommandHandler(
        IFileSystem fileSystem,
        ICopyFileIntentionHandler handler,
        IErrorShower errorShower
    )
    {
        _fileSystem = fileSystem;
        _handler = handler;
        _errorShower = errorShower;
    }

    /// <summary>
    /// コマンドを実行する。
    /// </summary>
    public async Task ExecuteAsync(CopyCommandArguments arguments)
    {
        var intention = CreateIntention(arguments);
        if (intention.IsFailed)
        {
            await _errorShower.ShowErrorsAsync(intention.Errors);
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
}