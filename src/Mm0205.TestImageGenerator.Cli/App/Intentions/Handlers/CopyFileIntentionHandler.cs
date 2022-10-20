using System.IO.Abstractions;
using Mm0205.TestImageGenerator.Cli.Domain.Services;

namespace Mm0205.TestImageGenerator.Cli.App.Intentions.Handlers;

public class CopyFileIntentionHandler : ICopyFileIntentionHandler
{
    private readonly IFileSystem _fileSystem;
    private readonly FileNameRuleService _fileNameRuleService;

    public CopyFileIntentionHandler(
        IFileSystem fileSystem,
        FileNameRuleService fileNameRuleService
    )
    {
        _fileSystem = fileSystem;
        _fileNameRuleService = fileNameRuleService;
    }

    public Task ExecuteAsync(CopyFileIntention intention)
    {
        return Task.Run(async () =>
        {
            var outputFilePaths = _fileNameRuleService.CreateOutputFilePaths(
                intention.SourceFilePath,
                intention.DestinationFolderPath,
                intention.StartIndex,
                intention.Count
            );

            foreach (var outputFilePath in outputFilePaths)
            {
                await Task.Run(() =>
                {
                    _fileSystem.File.Copy(intention.SourceFilePath.Value, outputFilePath);
                });
            }
        });
    }
}