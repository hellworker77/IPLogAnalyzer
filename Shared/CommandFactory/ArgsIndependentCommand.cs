using FluentValidation;
using Microsoft.Extensions.Configuration;
using Shared.Models;

namespace Shared.CommandFactory;

public class ArgsIndependentCommand : AbstractCommand
{
    private readonly IConfigurationRoot _configuration;

    public ArgsIndependentCommand(IConfigurationRoot configuration, 
        IValidator<Options> validator) : base(validator)
    {
        _configuration = configuration;
    }

    public override Options? ExecuteCommand()
    {
        throw new NotImplementedException();
    }
}