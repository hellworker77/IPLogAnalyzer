using Shared.Managers.Interfaces;
using Shared.Models;

namespace Shared.Managers;

public class IpAnalyzerFilter : IIpAnalyzerFilter
{
    public IList<LogEntry> FilterLogs(IList<LogEntry> logs, AnalyzerOptions options)
        => logs.Where(log =>
            IsAddressMatches(log.Ipv4Address, options.AddressStart, options.AddressMask) &&
            IsDateMatches(log.Date, options.DateStart, options.DateEnd)).ToList();

    public Dictionary<string, int> CollectByQuantity(IList<LogEntry> logs)
    {
        var quantities = new Dictionary<string, int>();
        foreach (var log in logs)
        {
            quantities.TryAdd(log.Ipv4Address, 0);
            quantities[log.Ipv4Address]++;
        }

        return quantities;
    }

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