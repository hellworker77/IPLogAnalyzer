using CommandLine;

namespace Shared.Models;

public class Options
{
    [Option("file-log", Required = true, HelpText = "Path to the log file.")]
    public string FileLog { get; set; }
    
    [Option("file-output", Required = true, HelpText = "Path to the output file.")]
    public string FileOutput { get; set; }
    
    [Option("address-start", HelpText = "Start address for IP range.")]
    public string AddressStart { get; set; }
    
    [Option("address-mask", HelpText = "Subnet mask for IP range.")]
    public int? AddressMask { get; set; }
}