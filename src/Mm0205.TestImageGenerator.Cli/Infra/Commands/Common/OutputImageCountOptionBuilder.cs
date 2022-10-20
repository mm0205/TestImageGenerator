using System.CommandLine;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public static class OutputImageCountOptionBuilder
{
    public static Option<int> Build(int defaultValue = 10)
    {
        var countOption = new Option<int>(
            name: "--count",
            description: $"出力画像数 [default: {defaultValue}]",
            getDefaultValue: () => defaultValue);
        countOption.AddAlias("-c");
        return countOption;       
    }
}