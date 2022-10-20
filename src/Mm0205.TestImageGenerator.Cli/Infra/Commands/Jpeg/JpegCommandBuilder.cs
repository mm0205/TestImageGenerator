using System.CommandLine;
using System.IO.Abstractions;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Jpeg;

public class JpegCommandBuilder : ISubCommandBuilder
{
    private readonly IFileSystem _fileSystem;
    private readonly JpegCommandHandler _handler;

    public JpegCommandBuilder(JpegCommandHandler handler, IFileSystem fileSystem)
    {
        _handler = handler;
        _fileSystem = fileSystem;
    }

    public string Name => "jpeg";

    public Command Build()
    {
        var templateOption = new Option<string?>(
            name: "--template",
            description: "テンプレート画像指定",
            getDefaultValue: () => null
        );
        templateOption.AddAlias("-t");

        var destOption = DestinationFolderPathOptionBuilder.Build(_fileSystem);
        var countOption = OutputImageCountOptionBuilder.Build();
        var startIndexOption = OutputStartIndexOptionBuilder.Build();

        var command = new Command(Name, "JPEG画像生成")
        {
            templateOption,
            destOption,
            countOption,
            startIndexOption
        };

        command.SetHandler(async (templateFile, destination, count, startIndex) =>
            {
                try
                {
                    await _handler.ExecuteAsync(
                        new JpegCommonArgs(
                            templateFile,
                            destination,
                            count,
                            startIndex));
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
            },
            templateOption,
            destOption,
            countOption,
            startIndexOption);

        return command;
    }
}