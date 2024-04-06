using FluentValidation;
using Shared.Models;

namespace Shared.Feature.Validators;

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
            .Must(ValidationExtensions.IsValidIpv4Address)
            .When(x=>x.AddressMask.HasValue)
            .WithMessage("Address start is required when address mask is provided.\nAddress start is not a valid IPv4 address.");

        RuleFor(x => x.AddressMask)
            .GreaterThan(0)
            .When(x => !string.IsNullOrEmpty(x.AddressStart)) 
            .WithMessage("Address mask must be greater than 0.")
            .LessThanOrEqualTo(32)
            .When(x => x.AddressMask.HasValue) 
            .WithMessage("Address mask must be less than or equal to 32.");
        
        RuleFor(x =>x.DateStart)
            .NotEmpty().WithMessage("Start date could not be empty")
            .Matches(@"^\d{2}\.\d{2}\.\d{4}$").WithMessage("Wrong date format (estimated: dd.MM.yyyy)")
            .Must(BeValidDate).WithMessage("Bad date");
        
        RuleFor(x =>x.DateEnd)
            .NotEmpty().WithMessage("Start date could not be empty")
            .Matches(@"^\d{2}\.\d{2}\.\d{4}$").WithMessage("Wrong date format (estimated: dd.MM.yyyy)")
            .Must(BeValidDate).WithMessage("Bad date");
        
    }
    private bool BeValidDate(string dateString)
    {
        return DateTime.TryParseExact(dateString, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out _);
    }
}