using CommandLine;

namespace Shared.Models;

public class Options
{
    [Option("file-log", Required = true)] 
    public string FileLog { get; set; }

    [Option("file-output", Required = true)]
    public string FileOutput { get; set; }

    [Option("start-date", Required = true)]
    public string DateStart { get; set; }

    [Option("end-date", Required = true)] 
    public string DateEnd { get; set; }
    
    [Option("address-start")] 
    public string AddressStart { get; set; }

    [Option("address-mask")] 
    public int? AddressMask { get; set; }
}