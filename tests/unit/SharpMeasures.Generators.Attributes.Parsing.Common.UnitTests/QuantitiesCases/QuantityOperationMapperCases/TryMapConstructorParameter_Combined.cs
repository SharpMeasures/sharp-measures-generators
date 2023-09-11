namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityOperationMapperCases;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System.Collections.Generic;

using Xunit;

public sealed class TryMapConstructorParameter_Combined
{
    private static IMappedCombinedConstructorArgumentRecorder? Target(ICombinedMapper<IQuantityOperationRecordBuilder> mapper, IParameterSymbol parameter, IQuantityOperationRecordBuilder recordBuilder) => mapper.TryMapConstructorParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapConstructorParameter_Combined(IAdaptiveMapperDependencyProvider<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == string.Empty), Mock.Of<IQuantityOperationRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void OperatorType_OperatorType_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = OperatorType.Addition;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, OperatorTypeParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithOperatorType(argument, syntax), Times.Once);
    }

    [Fact]
    public void OperatorType_OperatorType_TryRecordParamsArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, OperatorTypeParameter, Mock.Of<IQuantityOperationRecordBuilder>());

        var outcome = recorder!.TryRecordParamsArgument(OperatorType.Addition, Mock.Of<IReadOnlyList<ExpressionSyntax>>());

        Assert.False(outcome);
    }

    [Fact]
    public void OperatorType_OperatorType_TryRecordDefaultArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, OperatorTypeParameter, Mock.Of<IQuantityOperationRecordBuilder>());

        var outcome = recorder!.TryRecordDefaultArgument(true);

        Assert.False(outcome);
    }

    [Fact]
    public void OperatorType_Object_TryRecordArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, OperatorTypeParameter, Mock.Of<IQuantityOperationRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(Mock.Of<object>(), ExpressionSyntaxFactory.Create());

        Assert.False(outcome);
    }

    private static IParameterSymbol OperatorTypeParameter { get; } = Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == nameof(QuantityOperationAttribute<object, object>.OperatorType));
}
