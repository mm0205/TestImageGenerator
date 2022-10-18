namespace Mm0205.TestImageGenerator.Cli.App.Intentions;

public interface ICopyFileIntentionHandler
{
    Task ExecuteAsync(CopyFileIntention intention);
}