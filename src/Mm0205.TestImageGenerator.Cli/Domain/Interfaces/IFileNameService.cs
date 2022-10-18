using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.Domain.Interfaces;

public interface IFileNameService
{
    FilePathComponents SplitFilePath(IInputFilePath sourceFilePath);
    
    string Combine(IOutputFolderPath destFolderPath, string outputFileName);
}