using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Infra.Commands;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Copies;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Jpeg;
using Mm0205.TestImageGenerator.Cli.Infra.Images;

namespace Mm0205.TestImageGenerator.Cli.Infra;

public static class AddInfraExtension
{
    public static IServiceCollection AddInfra(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFileSystem, FileSystem>();
        serviceCollection.AddScoped<IErrorShower, ErrorShower>();
        
        serviceCollection.AddScoped<IJpegService, JpegService>();
        
        serviceCollection.AddScoped<RootCommandBuilder>();
        serviceCollection.AddScoped<CopyCommandHandler>();
        serviceCollection.AddScoped<CopyCommandBuilder>();

        serviceCollection.AddScoped<JpegCommandHandler>();
        serviceCollection.AddScoped<JpegCommandBuilder>();

        serviceCollection.AddScoped<Func<IEnumerable<ISubCommandBuilder>>>((sp) =>
        {
            return () =>
            {
                var result = new List<ISubCommandBuilder>
                {
                    sp.GetRequiredService<CopyCommandBuilder>(),
                    sp.GetRequiredService<JpegCommandBuilder>()
                };
                return result;
            };
        });

        return serviceCollection;
    }
}