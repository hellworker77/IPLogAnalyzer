using CommandLine;
using FluentValidation;
using Shared.Models;

namespace Shared.CommandFactory;

public class ArgsDependentCommand : AbstractCommand
{
    private readonly string[] _args;
    
    public ArgsDependentCommand(string[] args,
        IValidator<Options> validator) : base(validator)
    {
        _args = args;
    }
    
    public override Options? ExecuteCommand()
    {
        var parserResult = Parser.Default.ParseArguments<Options>(_args);

        if (parserResult.Errors.Any())
        {

            foreach (var error in parserResult.Errors)
            {
                Console.WriteLine(error);
            }
            return null;
        }

        if (Validate(parserResult.Value))
        {
            return parserResult.Value;
        }

        return null;
    }
}