namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityOperationMapperCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapTypeParameter_Semantic
{
    private static IMappedSemanticTypeArgumentRecorder? Target(ISemanticMapper<ISemanticQuantityOperationRecordBuilder> mapper, ITypeParameterSymbol parameter, ISemanticQuantityOperationRecordBuilder recordBuilder) => mapper.TryMapTypeParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapTypeParameter_Semantic(IAdaptiveMapperDependencyProvider<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == -1 && symbol.Name == string.Empty), Mock.Of<ISemanticQuantityOperationRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void Result_Type_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = Mock.Of<ITypeSymbol>();
        Mock<ISemanticQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, ResultParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithResult(argument), Times.Once);
    }

    [Fact]
    public void Other_Type_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = Mock.Of<ITypeSymbol>();
        Mock<ISemanticQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, OtherParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithOther(argument), Times.Once);
    }

    private static ITypeParameterSymbol ResultParameter { get; } = Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == 0 && symbol.Name == string.Empty);
    private static ITypeParameterSymbol OtherParameter { get; } = Mock.Of<ITypeParameterSymbol>(static (symbol) => symbol.Ordinal == 1 && symbol.Name == string.Empty);
}
