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
        throw new NotImplementedException();
    }

    
}