using System.IO.Abstractions;
using FluentResults;
using Mm0205.TestImageGenerator.Cli.App.Intentions;
using Mm0205.TestImageGenerator.Cli.Domain.Models;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;

namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Jpeg;

public class JpegCommandHandler
{
    private readonly IFileSystem _fileSystem;
    private readonly IErrorShower _errorShower;
    private readonly ICreateJpegIntentionHandler _intentionHandler;

    public JpegCommandHandler(IFileSystem fileSystem, IErrorShower errorShower, ICreateJpegIntentionHandler intentionHandler)
    {
        _fileSystem = fileSystem;
        _errorShower = errorShower;
        _intentionHandler = intentionHandler;
    }

    public async Task ExecuteAsync(JpegCommonArgs args)
    {
        var intention = CreateIntention(args);
        if (intention.IsFailed)
        {
            await _errorShower.ShowErrorsAsync(intention.Errors);
            return;
        }

        await _intentionHandler.ExecuteAsync(intention.Value);
    }

    private Result<CreateJpegIntention> CreateIntention(JpegCommonArgs args)
    {
        var templateFilePath = InputImageFilePath.Create(_fileSystem, args.TemplateFilePath);
        if (templateFilePath.IsFailed)
        {
            return Result.Fail<CreateJpegIntention>(templateFilePath.Errors);
        }

        var outputFolderPath = OutputFolderPath.Create(_fileSystem, args.OutputFolderPath);
        if (templateFilePath.IsFailed)
        {
            return Result.Fail<CreateJpegIntention>(outputFolderPath.Errors);
        }

        var count = OutputFileCount.Create(args.OutputCount);
        if (count.IsFailed)
        {
            return Result.Fail<CreateJpegIntention>(count.Errors);
        }

        var startIndex = OutputIndex.Create(args.StartIndex);
        if (startIndex.IsFailed)
        {
            return Result.Fail<CreateJpegIntention>(startIndex.Errors);
        }

        return new CreateJpegIntention(templateFilePath.Value,
            outputFolderPath.Value,
            count.Value,
            startIndex.Value);
    }
}