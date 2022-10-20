using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.App.Intentions.Handlers;

public class CreateJpegIntentionHandler : ICreateJpegIntentionHandler
{
    private readonly IFileNameService _fileNameService;
    private readonly IJpegService _jpegService;

    public CreateJpegIntentionHandler(IFileNameService fileNameService, IJpegService jpegService)
    {
        _fileNameService = fileNameService;
        _jpegService = jpegService;
    }

    public async Task<Result> ExecuteAsync(CreateJpegIntention intention)
    {
        return await Task.Run(async () =>
        {
            var filePaths = intention.StartIndex.CreateRange(intention.OutputCount)
                .Select(index => OutputJpeg.Create(_fileNameService, intention.OutputFolderPath, index))
                .ToArray();

            var filePathsResult = Result.Merge(filePaths);
            if (filePathsResult.IsFailed)
            {
                return Result.Fail(filePathsResult.Errors);
            }

            foreach (var chunk in filePathsResult.Value.Chunk(10))
            {
                var tasks = chunk.Select(x => _jpegService.CreateAsync(x, intention.TemplateFilePath));
                ResultBase[] results = await Task.WhenAll(tasks);
                
                var merged = Result.Merge(results);
                if (merged.IsFailed)
                {
                    return merged;
                }
            }

            return Result.Ok();
        });

    }
}