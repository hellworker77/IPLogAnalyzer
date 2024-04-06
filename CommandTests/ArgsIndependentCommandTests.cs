using FluentValidation;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Shared.CommandFactory;
using Shared.Models;

namespace CommandTests;

public class ArgsIndependentCommandTests
{
    [Fact]
    public void ExecuteCommand_ConfigurationIsValid_ReturnsAnalyzerOptions()
    {
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var validatorMock = new Mock<IValidator<Options>>();
        validatorMock.Setup(v => v.Validate(It.IsAny<Options>())).Returns(new FluentValidation.Results.ValidationResult());

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<AnalyzerOptions>(It.IsAny<Options>())).Returns(new AnalyzerOptions());

        var command = new ArgsIndependentCommand(configuration, mapperMock.Object, validatorMock.Object);

        var result = command.ExecuteCommand();

        Assert.NotNull(result);
    }
}