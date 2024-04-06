namespace Shared.Managers.Interfaces;

public interface IIpAnalyzerFileWorker
{
    IEnumerable<string> ReadData(string path);
    void WriteData(Dictionary<string, int> data, string path);
}