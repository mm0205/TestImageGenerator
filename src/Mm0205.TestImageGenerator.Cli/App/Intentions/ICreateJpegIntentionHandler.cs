using FluentResults;

namespace Mm0205.TestImageGenerator.Cli.App.Intentions;

public interface ICreateJpegIntentionHandler
{
    Task<Result> ExecuteAsync(CreateJpegIntention intention);
}