using System.CommandLine;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public static class OutputStartIndexOptionBuilder
{
    public static Option<int> Build(int defaultValue = 10)
    {
        var startIndexOption = new Option<int>(
            name: "--start",
            description: "開始番号 [default: 1]",
            getDefaultValue: () => 1);
        return startIndexOption;       
    }
}