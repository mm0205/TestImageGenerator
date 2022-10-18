using System.CommandLine;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands;

/// <summary>
/// コマンドビルダーインタフェース。
/// </summary>
public interface ISubCommandBuilder
{
    /// <summary>
    /// コマンド名。
    /// </summary>
    string Name { get; }
    
    Command Build();
}