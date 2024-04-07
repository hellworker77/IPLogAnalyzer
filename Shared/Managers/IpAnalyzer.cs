using Shared.CommandFactory;
using Shared.Extensions;
using Shared.Managers.Interfaces;

namespace Shared.Managers;

public class IpAnalyzer : IIpAnalyzer
{
    private readonly Func<AbstractCommand> _commandFactory;
    private readonly IIpAnalyzerFilter _filter;
    private readonly IIpAnalyzerParser _parser;
    private readonly IIpAnalyzerFileWorker _fileWorker;
    
    private const char LineSeparator = ':';

    public IpAnalyzer(Func<AbstractCommand> commandFactory,
        IIpAnalyzerFilter filter,
        IIpAnalyzerParser parser,
        IIpAnalyzerFileWorker fileWorker)
    {
        _commandFactory = commandFactory;
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
            PrintExtensions.PrintError("Options not provided");
            return;
        }
        
        var rawData = _fileWorker
            .ReadData(options.FileLog)
            .ToList();
        PrintExtensions.PrintInfo($"Data received form file {rawData.Count()} note(s).");
        
        var logs = _parser.Parse(rawData);
        PrintExtensions.PrintInfo($"Data parsed {logs.Count} note(s).");
        
        logs = _filter.FilterLogs(logs, options);
        PrintExtensions.PrintInfo($"Data filtered {logs.Count} note(s).");
        
        var data = _filter.CollectByQuantity(logs);
        _fileWorker.WriteData(data, options.FileOutput);
        PrintExtensions.PrintSuccess($"Data wrote to file {data.Count} note(s).");
    }
}