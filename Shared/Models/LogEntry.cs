namespace Shared.Models;

public record LogEntry
{
    public string Ipv4Address { get; set; }
    public DateTime Date { get; set; }
}