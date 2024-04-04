using CommandLine;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Shared.CommandFactory;
using Shared.Extensions;
using Shared.Feature;
using Shared.Models;

var serviceCollection = new ServiceCollection();

serviceCollection.AddShared();
serviceCollection.AddSingleton(args);

var provider = serviceCollection.BuildServiceProvider();

var commandFactory = provider.GetService<Func<string[], AbstractCommand>>();
if (commandFactory is null)
{
    Console.WriteLine("Command for executing not found :(");
    return;
}

var command = commandFactory(args);
var options = command.ExecuteCommand();

if (options is null)
{
    return;
}



Console.ReadKey();

