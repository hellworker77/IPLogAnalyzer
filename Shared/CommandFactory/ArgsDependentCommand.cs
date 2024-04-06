using CommandLine;
using FluentValidation;
using MapsterMapper;
using Shared.Extensions;
using Shared.Models;

namespace Shared.CommandFactory;

public class ArgsDependentCommand : AbstractCommand
{
    private readonly string[] _args;
    private readonly IMapper _mapper;
    
    public ArgsDependentCommand(string[] args,
        IMapper mapper,
        IValidator<Options> validator) : base(validator)
    {
        _args = args;
        _mapper = mapper;
    }
    
    public override AnalyzerOptions? ExecuteCommand()
    {
        var parserResult = Parser.Default.ParseArguments<Options>(_args);

        if (parserResult.Errors.Any())
        {
            return null;
        }

        if (Validate(parserResult.Value))
        {
            
            return _mapper.Map<AnalyzerOptions>(parserResult.Value);
        }

        return null;
    }
}