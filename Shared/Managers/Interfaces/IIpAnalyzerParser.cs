using System.Collections;
using Shared.Models;

namespace Shared.Managers.Interfaces;

public interface IIpAnalyzerParser
{
    IList<LogEntry> Parse(IEnumerable<string> rawData);
}