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

    protected override bool CheckIsArgsNotEmpty(string[] args)
    {
        return true;
    }

    public override Options? ExecuteCommand()
    {
        var options = new Options();
        _configuration.GetSection("Options").Bind(options);
        
        if (Validate(options))
        {
            return options;
        }

        return null;
    }
}