using FluentResults;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

public interface IErrorShower
{
    Task ShowErrorsAsync(IEnumerable<IError> errors);
}