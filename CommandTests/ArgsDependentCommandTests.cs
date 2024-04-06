using FluentValidation;
using MapsterMapper;
using Moq;
using Shared.CommandFactory;
using Shared.Models;

namespace CommandTests;

public class ArgsDependentCommandTests
{
    [Fact]
    public void ExecuteCommand_ValidArgs_ReturnsAnalyzerOptions()
    {
        var args = new string[] {
            "--file-log", @"A:\Log.txt",
            "--file-output", @"A:\Output.txt",
            "--address-start", "127.0.0.2",
            "--address-mask", "24",
            "--start-date", "29.04.2002",
            "--end-date", "20.12.2012"
        };
        
        var validatorMock = new Mock<IValidator<Options>>();
        validatorMock.Setup(v => v.Validate(It.IsAny<Options>())).Returns(new FluentValidation.Results.ValidationResult());

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AnalyzerOptions>(It.IsAny<Options>())).Returns(new AnalyzerOptions());

        var command = new ArgsDependentCommand(args, mapperMock.Object, validatorMock.Object);

        var result = command.ExecuteCommand();

        Assert.NotNull(result);
    }
}