using System.CommandLine;
using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Mm0205.TestImageGenerator.Cli.App.Intentions;
using Mm0205.TestImageGenerator.Cli.App.Intentions.Handlers;
using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Domain.Services;
using Mm0205.TestImageGenerator.Cli.Infra.Commands;
using Mm0205.TestImageGenerator.Cli.Infra.Commands.Copies;
using Mm0205.TestImageGenerator.Cli.Infra.Io;

var serviceCollection = new ServiceCollection();

// ドメイン
serviceCollection.AddScoped<IFileNameService, FileNameService>();
serviceCollection.AddScoped<FileNameRuleService>();

// App レイヤー
serviceCollection.AddScoped<ICopyFileIntentionHandler, CopyFileIntentionHandler>();

// Infraレイヤー
serviceCollection.AddSingleton<IFileSystem, FileSystem>();
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


var serviceProvider = serviceCollection.BuildServiceProvider();

await using var scope = serviceProvider.CreateAsyncScope();

var rootCommandBuilder = scope.ServiceProvider.GetRequiredService<RootCommandBuilder>();
var rootCommand = rootCommandBuilder.Build();
await rootCommand.InvokeAsync(args);