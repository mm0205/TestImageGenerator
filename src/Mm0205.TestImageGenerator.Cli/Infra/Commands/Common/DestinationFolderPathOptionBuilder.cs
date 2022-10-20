using System.CommandLine;
using System.IO.Abstractions;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public class DestinationFolderPathOptionBuilder
{
    public static Option<string> Build(IFileSystem fileSystem)
    {
        var destOption = new Option<string>(
            name: "--destination",
            description: "出力先フォルダパス [default: ./out]",
            getDefaultValue: () => fileSystem.Path.Combine(
                fileSystem.Directory.GetCurrentDirectory(),
                "out"
            ));
        destOption.AddAlias("-d");

        return destOption;
    }
}