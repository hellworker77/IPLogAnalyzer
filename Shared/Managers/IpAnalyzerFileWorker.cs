using Shared.Managers.Interfaces;

namespace Shared.Managers;

public class IpAnalyzerFileWorker : IIpAnalyzerFileWorker
{
    public IEnumerable<string> ReadData(string path)
        =>File.ReadLines(path) ?? Array.Empty<string>();

    public void WriteData(Dictionary<string, int> data, string path)
    {
        using StreamWriter streamWriter = new StreamWriter(path);
        foreach (var item in data)
        {
            streamWriter.WriteLine($"{item.Key}:{item.Value}");
        }
    }
}