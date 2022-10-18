using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.Domain.Services;

public class FileNameRuleService
{
    private readonly IFileNameService _fileNameService;

    public FileNameRuleService(IFileNameService fileNameService)
    {
        _fileNameService = fileNameService;
    }

    public IEnumerable<string> CreateOutputFilePaths(
        IInputFilePath sourceFilePath,
        IOutputFolderPath destinationFolderPath,
        OutputIndex startIndex,
        OutputFileCount count
    )
        => startIndex.CreateRange(count)
            .Select(
                x => CreateOutputPathForIndex(
                    sourceFilePath,
                    destinationFolderPath,
                    x
                )
            );

    private string CreateOutputPathForIndex(
        IInputFilePath sourceFilePath,
        IOutputFolderPath destFolderPath,
        OutputIndex index
    )
    {
        var filePathComponents = _fileNameService.SplitFilePath(sourceFilePath);
        var outputFileName = $"{filePathComponents.Name}_{index.Value:D5}{filePathComponents.Extension}";
        return _fileNameService.Combine(destFolderPath, outputFileName);
    }
}