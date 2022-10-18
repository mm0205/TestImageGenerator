using System.CommandLine;
using System.IO.Abstractions;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Copies;

/// <summary>
/// コピーコマンドビルダー。
/// </summary>
public class CopyCommandBuilder : ISubCommandBuilder
{
    public string Name => "copy";

    private const string Description = @"指定されたファイルを、指定数コピーする。";

    private readonly IFileSystem _fileSystem;

    private readonly CopyCommandHandler _copyCommandHandler;

    public CopyCommandBuilder(
        IFileSystem fileSystem,
        CopyCommandHandler copyCommandHandler
    )
    {
        _fileSystem = fileSystem;
        _copyCommandHandler = copyCommandHandler;
    }

    public Command Build()
    {
        var command = new Command(Name, Description);

        var sourceOption = new Option<string>(
            name: "--source",
            description: "コピー元ファイルパス")
        {
            IsRequired = true
        };
        sourceOption.AddAlias("-s");
        command.AddOption(sourceOption);

        var destOption = new Option<string>(
            name: "--destination",
            description: "出力先フォルダパス [default: ./out]",
            getDefaultValue: () => _fileSystem.Path.Combine(
                _fileSystem.Directory.GetCurrentDirectory(),
                "out"
            ));
        destOption.AddAlias("-d");
        command.AddOption(destOption);

        var countOption = new Option<int>(
            name: "--count",
            description: "コピー数 [default: 10]",
            getDefaultValue: () => 10);
        countOption.AddAlias("-c");
        command.AddOption(countOption);

        var startIndexOption = new Option<int>(
            name: "--start",
            description: "開始番号 [default: 1]",
            getDefaultValue: () => 1);
        command.AddOption(startIndexOption);

        command.SetHandler(async (sourcePath, destPath, count, startIndex) =>
            {
                await _copyCommandHandler.ExecuteAsync(new CopyCommandArguments(
                    sourcePath,
                    destPath,
                    count,
                    startIndex));
            },
            sourceOption,
            destOption,
            countOption,
            startIndexOption);

        return command;
    }
}