using FluentValidation;
using FluentValidation.Results;
using Moq;
using Shared.Managers;
using Shared.Models;

namespace ManagerTests;

public class IpAnalyzerParserTests
{
    [Fact]
    public void Parse_ValidData_ReturnsListOfLogEntries()
    {
        var validatorMock = new Mock<IValidator<LogEntry>>();
        validatorMock.Setup(v => v.Validate(It.IsAny<LogEntry>())).Returns(new ValidationResult());
        var parser = new IpAnalyzerParser(validatorMock.Object);
        var rawData = new List<string>
        {
            "192.168.1.1:2022-01-01",
            "192.168.1.2:2022-01-02",
            "192.168.1.3:2022-01-03"
        };

        var result = parser.Parse(rawData);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Parse_InvalidData_DoesNotAddEntryToList()
    {
        var validatorMock = new Mock<IValidator<LogEntry>>();
        validatorMock.Setup(v => v.Validate(It.IsAny<LogEntry>()))
            .Returns(new ValidationResult(new List<ValidationFailure>()));

        var parser = new IpAnalyzerParser(validatorMock.Object);
        var rawData = new List<string>
        {
            "192.168.1.1:2022-01-01",
            "InvalidData",
            "192.168.1.3:2022-01-03"
        };

        var result = parser.Parse(rawData);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
}