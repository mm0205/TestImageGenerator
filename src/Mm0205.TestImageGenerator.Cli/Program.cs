using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Mm0205.TestImageGenerator.Cli.App.Intentions;
using Mm0205.TestImageGenerator.Cli.App.Intentions.Handlers;
using Mm0205.TestImageGenerator.Cli.Domain.Interfaces;
using Mm0205.TestImageGenerator.Cli.Domain.Services;
using Mm0205.TestImageGenerator.Cli.Infra;
using Mm0205.TestImageGenerator.Cli.Infra.Commands;
using Mm0205.TestImageGenerator.Cli.Infra.Io;

var serviceCollection = new ServiceCollection();

// ドメイン
serviceCollection.AddScoped<IFileNameService, FileNameService>();
serviceCollection.AddScoped<FileNameRuleService>();

// App レイヤー
serviceCollection.AddScoped<ICopyFileIntentionHandler, CopyFileIntentionHandler>();
serviceCollection.AddScoped<ICreateJpegIntentionHandler, CreateJpegIntentionHandler>();

// Infraレイヤー
serviceCollection.AddInfra();


var serviceProvider = serviceCollection.BuildServiceProvider();

await using var scope = serviceProvider.CreateAsyncScope();

var rootCommandBuilder = scope.ServiceProvider.GetRequiredService<RootCommandBuilder>();
var rootCommand = rootCommandBuilder.Build();
await rootCommand.InvokeAsync(args);