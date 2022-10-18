using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.Infra.Io;

public class FileNameService : IFileNameService
{
    public FilePathComponents SplitFilePath(IInputFilePath sourceFilePath)
    {
        var name = Path.GetFileNameWithoutExtension(sourceFilePath.Value);
        var ext = Path.GetExtension(sourceFilePath.Value);
        return new FilePathComponents(name, ext);
    }

    public string Combine(IOutputFolderPath destFolderPath, string outputFileName)
        => Path.Combine(destFolderPath.FolderPath, outputFileName);
}