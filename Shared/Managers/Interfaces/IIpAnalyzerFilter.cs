using Shared.Models;

namespace Shared.Managers.Interfaces;

public interface IIpAnalyzerFilter
{
    void FilterLogs(IList<LogEntry> logs, AnalyzerOptions options);
    Dictionary<string, int> CollectByQuantity(IList<LogEntry> logs);
}