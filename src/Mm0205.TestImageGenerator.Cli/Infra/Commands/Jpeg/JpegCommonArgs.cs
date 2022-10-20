namespace Mm0205.TestImageGenerator.Cli.Infra.Commands.Jpeg;

public record JpegCommonArgs(
    string? TemplateFilePath,
    string OutputFolderPath,
    int OutputCount,
    int StartIndex
);