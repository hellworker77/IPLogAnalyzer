using FluentValidation;
using Shared.CommandFactory;
using Shared.Extensions;
using Shared.Managers.Interfaces;
using Shared.Models;

namespace Shared.Managers;

public class IpAnalyzer : IIpAnalyzer
{
    private readonly Func<AbstractCommand> _commandFactory;
    private readonly IValidator<LogEntry> _validator;
    private readonly IIpAnalyzerFilter _filter;
    private readonly IIpAnalyzerParser _parser;
    private readonly IIpAnalyzerFileWorker _fileWorker;
    private IList<LogEntry> _logs = new List<LogEntry>();
    
    private const char LineSeparator = ':';

    public IpAnalyzer(Func<AbstractCommand> commandFactory,
        IValidator<LogEntry> validator,
        IIpAnalyzerFilter filter,
        IIpAnalyzerParser parser,
        IIpAnalyzerFileWorker fileWorker)
    {
        _commandFactory = commandFactory;
        _validator = validator;
        _filter = filter;
        _parser = parser;
        _fileWorker = fileWorker;
    }

    public void Analyze()
    {
        var selectedCommand = _commandFactory();
        var options = selectedCommand.ExecuteCommand();
        if (options is null)
        {
            return;
        }

        var rawData = _fileWorker.ReadData(options.FileLog);
        _logs = _parser.Parse(rawData);
        _filter.FilterLogs(_logs, options);
        var data = _filter.CollectByQuantity(_logs);
        _fileWorker.WriteData(data, options.FileOutput);
    }
}