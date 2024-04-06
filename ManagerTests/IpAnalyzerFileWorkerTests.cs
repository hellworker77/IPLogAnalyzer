using Shared.Managers;

namespace ManagerTests;

public class IpAnalyzerFileWorkerTests
{
    [Fact]
    public void ReadData_ValidFilePath_ReturnsLinesFromFile()
    {
        var fileWorker = new IpAnalyzerFileWorker();
        var filePath = "Files/in_test.txt";
        var exceptedLines = new string[]
        {
            "127.0.0.1:2004-05-12 14:24:52",
            "127.0.0.1:2004-05-12 14:24:42",
            "127.0.0.1:2004-05-12 14:24:32",
            "127.0.0.1:2004-05-12 14:24:22",
        };
        
        var actualLines = fileWorker.ReadData(filePath);
        
        Assert.Equal(exceptedLines, actualLines);
    }
    [Fact]
    public void WriteData_ValidData_WritesDataToFile()
    {
        var fileWorker = new IpAnalyzerFileWorker();
        var filePath = "Files/out_test.txt";
        var data = new Dictionary<string, int>
        {
            { "127.0.0.1", 13 },
            { "127.0.0.2", 2 },
        };
        var exceptedLines = new string[]
        {
            "127.0.0.1:13",
            "127.0.0.2:2"
        };
        
        fileWorker.WriteData(data, filePath);
        
        var actualLines = File.ReadAllLines(filePath);
        Assert.Equal(exceptedLines, actualLines); 
    }
}