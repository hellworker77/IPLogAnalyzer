using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Shared.Extensions;
using Shared.Models;

namespace Shared.CommandFactory;

public class ArgsIndependentCommand : AbstractCommand
{
    private readonly IConfigurationRoot _configuration;
    private readonly IMapper _mapper;

    public ArgsIndependentCommand(IConfigurationRoot configuration,
        IMapper mapper,
        IValidator<Options> validator) : base(validator)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    protected override bool DetermineCommandByArgs(string[] args) => true;

    public override AnalyzerOptions? ExecuteCommand()
    {
        PrintExtensions.PrintInfo("Args not received using configuration file");
        var options = new Options();
        _configuration.GetSection("Options").Bind(options);
        
        if (Validate(options))
        {
            return _mapper.Map<AnalyzerOptions>(options);
        }

        return null;
    }
}