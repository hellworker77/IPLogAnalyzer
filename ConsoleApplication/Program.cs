using CommandLine;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Shared.CommandFactory;
using Shared.Extensions;
using Shared.Feature;
using Shared.Managers.Interfaces;
using Shared.Models;

var serviceCollection = new ServiceCollection();

serviceCollection.AddShared();
serviceCollection.AddSingleton(args);

var provider = serviceCollection.BuildServiceProvider();

var ipAnalyzer = provider.GetService<IIpAnalyzer>();
if (ipAnalyzer is null)
{
    PrintExtensions.PrintError("Ip analyzer not found :(");
    Console.ReadKey();
    return;
}

ipAnalyzer.Analyze();

Console.ReadKey();