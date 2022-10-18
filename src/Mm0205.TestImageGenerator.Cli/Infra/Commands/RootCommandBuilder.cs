using System.CommandLine;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands;

public class RootCommandBuilder
{
    private readonly Func<IEnumerable<ISubCommandBuilder>> _getSubCommandBuilders;

    public RootCommandBuilder(
        Func<IEnumerable<ISubCommandBuilder>> getSubCommandBuilders
    )
    {
        _getSubCommandBuilders = getSubCommandBuilders;
    }

    public RootCommand Build()
    {
        var rootCommand = new RootCommand("テスト画像生成ツール");

        foreach (var subCommandBuilder in _getSubCommandBuilders())
        {
            rootCommand.AddCommand(subCommandBuilder.Build());
        }

        return rootCommand;
    }
}