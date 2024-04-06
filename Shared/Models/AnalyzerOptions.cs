namespace Shared.Models;

public class AnalyzerOptions
{
    public string FileLog { get; set; }
    public string FileOutput { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string AddressStart { get; set; }
    public int? AddressMask { get; set; }
}