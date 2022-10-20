using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.App.Intentions;

public record CreateJpegIntention(
    IInputImageFilePath? TemplateFilePath,
    IOutputFolderPath OutputFolderPath,
    OutputFileCount OutputCount,
    OutputIndex StartIndex
);