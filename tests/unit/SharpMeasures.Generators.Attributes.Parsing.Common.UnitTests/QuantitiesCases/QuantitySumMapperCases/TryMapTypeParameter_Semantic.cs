namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantitySumMapperCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapTypeParameter_Semantic
{
    private static IMappedSemanticTypeArgumentRecorder? Target(ISemanticMapper<ISemanticQuantitySumRecordBuilder> mapper, ITypeParameterSymbol parameter, ISemanticQuantitySumRecordBuilder recordBuilder) => mapper.TryMapTypeParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapTypeParameter_Semantic(IAdaptiveMapperDependencyProvider<IQuantitySumRecordBuilder, ISemanticQuantitySumRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == -1 && symbol.Name == string.Empty), Mock.Of<ISemanticQuantitySumRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void Sum_Type_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = Mock.Of<ITypeSymbol>();
        Mock<ISemanticQuantitySumRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, SumParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithSum(argument), Times.Once);
    }

    private static ITypeParameterSymbol SumParameter { get; } = Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == 0 && symbol.Name == string.Empty);
}
