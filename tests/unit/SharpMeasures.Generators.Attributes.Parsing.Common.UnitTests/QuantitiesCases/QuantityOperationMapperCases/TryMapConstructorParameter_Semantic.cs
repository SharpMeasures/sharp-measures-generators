namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityOperationMapperCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapConstructorParameter_Semantic
{
    private static IMappedSemanticConstructorArgumentRecorder? Target(ISemanticMapper<ISemanticQuantityOperationRecordBuilder> mapper, IParameterSymbol parameter, ISemanticQuantityOperationRecordBuilder recordBuilder) => mapper.TryMapConstructorParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapConstructorParameter_Semantic(IAdaptiveMapperDependencyProvider<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == string.Empty), Mock.Of<ISemanticQuantityOperationRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void OperatorType_OperatorType_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = OperatorType.Subtraction;
        Mock<ISemanticQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, OperatorTypeParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithOperatorType(argument), Times.Once);
    }

    [Fact]
    public void OperatorType_Object_TryRecordArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, OperatorTypeParameter, Mock.Of<ISemanticQuantityOperationRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(Mock.Of<object>());

        Assert.False(outcome);
    }

    private static IParameterSymbol OperatorTypeParameter { get; } = Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == nameof(QuantityOperationAttribute<object, object>.OperatorType));
}
