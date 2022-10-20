using FluentResults;
using Mm0205.TestImageGenerator.Cli.Domain.Models;

namespace Mm0205.TestImageGenerator.Cli.Domain.Interfaces;

public interface IJpegService
{
    Task<Result> CreateAsync(OutputJpeg outputJpeg, IInputImageFilePath? templateFilePath);
}