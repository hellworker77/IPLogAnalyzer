using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Managers.Interfaces;

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