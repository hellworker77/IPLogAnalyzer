using System.Globalization;
using Mapster;
using Shared.Models;

namespace Shared.Feature.Mappers;

public class OptionsMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Options, AnalyzerOptions>()
            .Map(destination => destination.DateStart,
                source => DateTime.ParseExact(source.DateStart, "dd.MM.yyyy", null,
                    DateTimeStyles.None))
            .Map(destination => destination.DateEnd,
                source => DateTime.ParseExact(source.DateEnd, "dd.MM.yyyy", null,
                    DateTimeStyles.None));
    }
}