using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.App.Intentions;

/// <summary>
/// コピーコマンド用の引数。
/// </summary>
/// <param name="SourceFilePath">コピー元ファイルパス。</param>
/// <param name="DestinationFolderPath">コピー先フォルダパス。</param>
/// <param name="Count">コピー数。</param>
/// <param name="StartIndex">開始インデックス。</param>
public record CopyFileIntention(
    IInputFilePath SourceFilePath,
    IOutputFolderPath DestinationFolderPath,
    OutputFileCount Count,
    OutputIndex StartIndex
);