using System.CommandLine;
using System.IO.Abstractions;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

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

        var sourceOption = new Option<string>(
            name: "--source",
            description: "コピー元ファイルパス")
        {
            IsRequired = true
        };
        sourceOption.AddAlias("-s");

        var destOption = new Option<string>(
            name: "--destination",
            description: "出力先フォルダパス [default: ./out]",
            getDefaultValue: () => _fileSystem.Path.Combine(
                _fileSystem.Directory.GetCurrentDirectory(),
                "out"
            ));
        destOption.AddAlias("-d");

        var countOption = OutputImageCountOptionBuilder.Build();
        var startIndexOption = OutputStartIndexOptionBuilder.Build();
        
        var command = new Command(Name, Description)
        {
            sourceOption,
            destOption,
            countOption,
            startIndexOption
        };

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