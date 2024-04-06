using Shared.Models;

namespace Shared.Managers.Interfaces;

public interface IIpAnalyzerFilter
{
    IList<LogEntry> FilterLogs(IList<LogEntry> logs, AnalyzerOptions options);
    Dictionary<string, int> CollectByQuantity(IList<LogEntry> logs);
}