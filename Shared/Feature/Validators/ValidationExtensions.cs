namespace Shared.Feature.Validators;

public static class ValidationExtensions
{
    private const char Ipv4Separator = '.';
    private const byte BytesInIpv4 = 4;
    public static bool IsValidIpv4Address(string address)
    {
        var parts = address?.Split(Ipv4Separator) ?? Array.Empty<string>();
        if (parts.Length != BytesInIpv4)
        {
            return false;
        }
        
        foreach (var part in parts)
        {
            if (!byte.TryParse(part, out var value))
            {
                return false;
            }
        }
        return true;
    }
}