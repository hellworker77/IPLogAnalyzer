using CommandLine;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.CommandFactory;
using Shared.Extensions;
using Shared.Managers.Interfaces;
using Shared.Models;

namespace Shared.Managers;

public class IpAnalyzer : IIpAnalyzer
{
    private readonly Func<AbstractCommand> _commandFactory;
    private readonly IValidator<LogEntry> _validator;
    private const char LineSeparator = ':';
    public IpAnalyzer(Func<AbstractCommand> commandFactory,
        IValidator<LogEntry> validator)
    {
        _commandFactory = commandFactory;
        _validator = validator;
    }

    public void Analyze()
    {
        var selectedCommand = _commandFactory();
        var options = selectedCommand.ExecuteCommand();
        if (options is null)
        {
            return;
        }

        var logs = GetLogs(options.FileLog);
        logs = FilterLogs(logs, options);
        PrintExtensions.PrintInfo($"Records found {logs.Count}");
        var quantities = CollectByQuantity(logs);
        WriteQuantities(quantities, options.FileOutput);
    }

    private IList<LogEntry> GetLogs(string path)
    {
        var logs = new List<LogEntry>();

        foreach (var line in File.ReadLines(path))
        {
            var firstColonIndex = line.IndexOf(LineSeparator);
            if (firstColonIndex == -1)
            {
                PrintExtensions.PrintError($"Unable to read line {line}");
                continue;
            }

            var ipAddress = line.Substring(0, firstColonIndex);
            var dateString = line.Substring(firstColonIndex + 1);
            if (!DateTime.TryParse(dateString, out var date))
            {
                PrintExtensions.PrintError($"Wrong date format {line} -> {dateString}");
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

    private void WriteQuantities(Dictionary<string, int> quantities, string path)
    {
        using StreamWriter streamWriter = new StreamWriter(path);
        foreach (var quantity in quantities)
        {
            streamWriter.WriteLine($"{quantity.Key}:{quantity.Value}");
        }
    }

    private Dictionary<string, int> CollectByQuantity(IList<LogEntry> logs)
    {
        var quantities = new Dictionary<string, int>();
        foreach (var log in logs)
        {
            quantities.TryAdd(log.Ipv4Address, 0);
            quantities[log.Ipv4Address]++;
        }

        return quantities;
    }

    private IList<LogEntry> FilterLogs(IList<LogEntry> logs, AnalyzerOptions options)
        => logs.Where(log =>
            IsAddressMatches(log.Ipv4Address, options.AddressStart, options.AddressMask) &&
            IsDateMatches(log.Date, options.DateStart, options.DateEnd)).ToList();

    private bool IsDateMatches(DateTime date, DateTime start, DateTime end)
        => date >= start && date <= end;

    private bool IsAddressMatches(string address, string startAddress, int? mask)
    {
        if (string.IsNullOrEmpty(startAddress))
        {
            return true;
        }

        if (!mask.HasValue)
        {
            return address == startAddress;
        }

        string[] addressParts = address.Split('.');
        string[] startAddressParts = startAddress.Split('.');

        uint maskBits = (uint.MaxValue << (32 - mask.Value)) & uint.MaxValue;
        
        uint ipv4Value = (uint.Parse(addressParts[0]) << 24) | (uint.Parse(addressParts[1]) << 16) |
                         (uint.Parse(addressParts[2]) << 8) | uint.Parse(addressParts[3]);
        uint startValue = (uint.Parse(startAddressParts[0]) << 24) | (uint.Parse(startAddressParts[1]) << 16) |
                          (uint.Parse(startAddressParts[2]) << 8) | uint.Parse(startAddressParts[3]);
        
        uint maskedAddressValue = ipv4Value & maskBits;
        uint maskedStartAddressValue = startValue & maskBits;
        
        return maskedAddressValue == maskedStartAddressValue;
    }
}