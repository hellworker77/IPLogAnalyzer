using Shared.Managers;
using Shared.Models;

namespace ManagerTests;

public class IpAnalyzerFilterTests
{
    [Fact]
    public void FilterLogs_FiltersLogsByOptions()
    {
        var filter = new IpAnalyzerFilter();
        var logs = new List<LogEntry>
        {
            new LogEntry { Ipv4Address = "192.168.1.1", Date = DateTime.Parse("2022-01-01") },
            new LogEntry { Ipv4Address = "192.168.1.2", Date = DateTime.Parse("2022-01-02") },
            new LogEntry { Ipv4Address = "192.168.1.3", Date = DateTime.Parse("2022-01-03") }
        };
        var options = new AnalyzerOptions
        {
            AddressStart = "192.168.1.2",
            DateStart = DateTime.Parse("2022-01-02"),
            DateEnd = DateTime.Parse("2022-01-03")
        };

        var filteredLogs = filter.FilterLogs(logs, options);
        
        Assert.Single(filteredLogs); 
        Assert.Contains(filteredLogs, log => log.Ipv4Address == "192.168.1.2");
    }
    [Fact]
    public void CollectByQuantity_CountsLogsByIpAddress()
    {
        var filter = new IpAnalyzerFilter(); 
        var logs = new List<LogEntry>
        {
            new LogEntry { Ipv4Address = "192.168.1.1", Date = DateTime.Parse("2022-01-01") },
            new LogEntry { Ipv4Address = "192.168.1.2", Date = DateTime.Parse("2022-01-02") },
            new LogEntry { Ipv4Address = "192.168.1.1", Date = DateTime.Parse("2022-01-03") },
            new LogEntry { Ipv4Address = "192.168.1.3", Date = DateTime.Parse("2022-01-03") }
        };
        
        var quantities = filter.CollectByQuantity(logs);
        
        Assert.Equal(2, quantities["192.168.1.1"]); 
        Assert.Equal(1, quantities["192.168.1.2"]); 
        Assert.Equal(1, quantities["192.168.1.3"]);
    }
}