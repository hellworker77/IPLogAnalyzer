using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Models;

namespace Shared.CommandFactory;

public abstract class AbstractCommand
{
    protected readonly IValidator<Options> _validator;

    protected AbstractCommand(IValidator<Options> validator)
    {
        _validator = validator;
    }

    protected virtual bool DetermineCommandByArgs(string[] args)
        => args.Length > 0;

    public abstract AnalyzerOptions? ExecuteCommand();

    protected virtual bool Validate(Options options)
    {
        var validationResult = _validator.Validate(options);
        if (validationResult.IsValid)
        {
            return true;
        }

        validationResult.Errors.Print();
        
        return false;
    }
    
    public static Func<IServiceProvider, Func<AbstractCommand>> GetCommand
        => provider =>
        {
            return () =>
            {
                var args = provider.GetService<string[]>() ?? Array.Empty<string>();
                var command = provider.GetServices<AbstractCommand>().First(c => c.DetermineCommandByArgs(args));

                return command;
            };
        };
}