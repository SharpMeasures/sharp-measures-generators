namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityDifferenceMapperCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapTypeParameter_Combined
{
    private static IMappedCombinedTypeArgumentRecorder? Target(ICombinedMapper<IQuantityDifferenceRecordBuilder> mapper, ITypeParameterSymbol parameter, IQuantityDifferenceRecordBuilder recordBuilder) => mapper.TryMapTypeParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapTypeParameter_Combined(IAdaptiveMapperDependencyProvider<IQuantityDifferenceRecordBuilder, ISemanticQuantityDifferenceRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == -1 && symbol.Name == string.Empty), Mock.Of<IQuantityDifferenceRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void Difference_Type_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = Mock.Of<ITypeSymbol>();
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityDifferenceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, DifferenceParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithDifference(argument, syntax), Times.Once);
    }

    private static ITypeParameterSymbol DifferenceParameter { get; } = Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == 0 && symbol.Name == string.Empty);
}
