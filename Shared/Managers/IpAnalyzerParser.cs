using FluentValidation;
using Shared.Extensions;
using Shared.Managers.Interfaces;
using Shared.Models;

namespace Shared.Managers;

public class IpAnalyzerParser : IIpAnalyzerParser
{
    private readonly IValidator<LogEntry> _validator;
    private const char LineSeparator = ':';

    public IpAnalyzerParser(IValidator<LogEntry> validator)
    {
        _validator = validator;
    }

    public IList<LogEntry> Parse(IEnumerable<string> rawData)
    {
        var logs = new List<LogEntry>();

        foreach (var item in rawData)
        {
            var firstColonIndex = item.IndexOf(LineSeparator);
            if (firstColonIndex == -1)
            {
                PrintExtensions.PrintError($"Unable to read data from {item}");
                continue;
            }

            var ipAddress = item.Substring(0, firstColonIndex);
            var dateString = item.Substring(firstColonIndex + 1);
            if (!DateTime.TryParse(dateString, out var date))
            {
                PrintExtensions.PrintError($"Wrong date format {item} -> {dateString}");
                continue;
            }

            var log = new LogEntry {Ipv4Address = ipAddress, Date = date};
            var validationResult = _validator.Validate(log);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.Print();
                continue;
            }

            logs.Add(log);
        }

        return logs;
    }
}