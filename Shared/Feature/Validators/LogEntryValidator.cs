using FluentValidation;
using Shared.Models;

namespace Shared.Feature.Validators;

public class LogEntryValidator: AbstractValidator<LogEntry>
{
    public LogEntryValidator()
    {
        RuleFor(x => x.Ipv4Address)
            .Must(ValidationExtensions.IsValidIpv4Address)
            .WithMessage("Address start is not a valid IPv4 address.");
    }
}