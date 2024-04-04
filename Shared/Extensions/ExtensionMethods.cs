using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CommandFactory;

namespace Shared.Extensions;

public static class ExtensionMethods
{
    public static IServiceCollection AddShared(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddArgsCommands()
            .AddConfiguration();
    private static IServiceCollection AddArgsCommands(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddTransient<AbstractCommand, ArgsDependentCommand>()
            .AddTransient<AbstractCommand, ArgsIndependentCommand>()
            .AddTransient(AbstractCommand.GetCommand);
    private static IServiceCollection AddConfiguration(this IServiceCollection serviceCollection)
    {
        try
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("configuration.json", optional: false, reloadOnChange: true);
            var config = builder.Build();
            serviceCollection.AddSingleton(config);
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("configuration.json doesn't exist");
        }

        return serviceCollection;
    }
}