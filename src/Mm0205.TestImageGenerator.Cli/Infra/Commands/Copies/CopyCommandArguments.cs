namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Copies;

/// <summary>
/// コピーコマンド用の引数。
/// </summary>
/// <param name="SourceFilePath">コピー元ファイルパス。</param>
/// <param name="DestinationFolderPath">コピー先フォルダパス。</param>
/// <param name="Count">コピー数。</param>
/// <param name="StartIndex">開始インデックス。</param>
public record CopyCommandArguments(
    string SourceFilePath,
    string DestinationFolderPath,
    int Count,
    int StartIndex
);