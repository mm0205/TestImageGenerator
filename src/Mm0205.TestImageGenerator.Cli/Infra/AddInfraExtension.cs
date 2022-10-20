using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Mm0205.TestImageGenerator.Cli.Infra.Commands;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Common;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Copies;

namespace Mm0205.TestImageGenerator.Cli.Infra;

public static class AddInfraExtension
{
    public static IServiceCollection AddInfra(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IFileSystem, FileSystem>();
        serviceCollection.AddScoped<IErrorShower, ErrorShower>();
        serviceCollection.AddScoped<RootCommandBuilder>();
        serviceCollection.AddScoped<CopyCommandHandler>();
        serviceCollection.AddScoped<CopyCommandBuilder>();
        
        serviceCollection.AddScoped<Func<IEnumerable<ISubCommandBuilder>>>((sp) =>
        {
            return () =>
            {
                var result = new List<ISubCommandBuilder>
                {
                    sp.GetRequiredService<CopyCommandBuilder>()
                };
                return result;
            };
        });

        return serviceCollection;
    }
}