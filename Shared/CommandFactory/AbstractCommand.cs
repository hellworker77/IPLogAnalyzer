using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Models;

namespace Shared.CommandFactory;

public abstract class AbstractCommand
{
    protected readonly IValidator<Options> _validator;

    protected AbstractCommand(IValidator<Options> validator)
    {
        _validator = validator;
    }

    protected virtual bool CheckIsPatternFor(string[] args)
        => args.Length > 0;

    public abstract Options? ExecuteCommand();

    protected virtual bool Validate(Options options)
    {
        var validationResult = _validator.Validate(options);
        if (validationResult.IsValid)
        {
            return true;
        }

        foreach (var validationFailure in validationResult.Errors)
        {
            Console.WriteLine(validationFailure.ErrorMessage);
        }

        return false;
    }
    public static Func<IServiceProvider, Func<string[], AbstractCommand>> GetCommand
        => provider => input =>
        {
            var command = provider.GetServices<AbstractCommand>().First(c => c.CheckIsPatternFor(input));

            return command;
        };
}