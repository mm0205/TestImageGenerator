namespace Mm0205.TestImageGenerator.Cli.Domain.Models;

/// <summary>
/// 出力先フォルダパス。
/// </summary>
public interface IOutputFolderPath
{
    /// <summary>
    /// 出力先フォルダパス。
    /// </summary>
    string FolderPath { get; }
}