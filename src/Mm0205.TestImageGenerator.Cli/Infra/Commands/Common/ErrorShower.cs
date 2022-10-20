using FluentResults;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public class ErrorShower : IErrorShower
{
    public async Task ShowErrorsAsync(IEnumerable<IError> errors)
    {
        foreach (var e in errors)
        {
            await Console.Error.WriteLineAsync(e.ToString());
        }
    }
}