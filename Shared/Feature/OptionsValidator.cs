using FluentValidation;
using Shared.Models;

namespace Shared.Feature;

public class OptionsValidator: AbstractValidator<Options>
{
    public OptionsValidator()
    {
        RuleFor(x => x.FileLog)
            .NotEmpty()
            .WithMessage("Log file path must not be empty.")
            .Must(File.Exists)
            .WithMessage("Log file does not exist.");

        RuleFor(x => x.FileOutput)
            .NotEmpty()
            .WithMessage("Output file path must not be empty.");

        RuleFor(x => x.AddressStart)
            .NotEmpty()
            .WithMessage("Address start must not be empty.")
            .When(x => x.AddressMask.HasValue) // Only validate if AddressMask is provided
            .WithMessage("Address start is required when address mask is provided.")
            .Must(IsValidIpv4Address)
            .When(x=>x.AddressMask.HasValue)
            .WithMessage("Address start is not a valid IPv4 address.");

        RuleFor(x => x.AddressMask)
            .GreaterThan(0)
            .When(x => !string.IsNullOrEmpty(x.AddressStart)) // Only validate if AddressStart is provided
            .WithMessage("Address mask must be greater than 0.")
            .LessThanOrEqualTo(32)
            .When(x => x.AddressMask.HasValue) // Only validate if provided
            .WithMessage("Address mask must be less than or equal to 32.");
    }
    private bool IsValidIpv4Address(string? address)
    {
        if (address is null)
        {
            return false;
        }
        var parts = address.Split('.');
        if (parts.Length != 4)
        {
            return false;
        }
        
        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var value) || value < 0 || value > 255)
            {
                return false;
            }
        }

        return true;
    }
}