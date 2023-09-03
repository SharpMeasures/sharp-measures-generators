namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceMapperCases;

using Microsoft.CodeAnalysis;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapConstructorParameter_Semantic
{
    private static IMappedSemanticConstructorArgumentRecorder? Target(ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder> mapper, IParameterSymbol parameter, ISemanticDefaultUnitInstanceRecordBuilder recordBuilder) => mapper.TryMapConstructorParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapConstructorParameter_Semantic(IAdaptiveMapperDependencyProvider<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == string.Empty), Mock.Of<ISemanticDefaultUnitInstanceRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void UnitInstance_String_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = string.Empty;
        Mock<ISemanticDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, GenerateSymbol, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithUnitInstance(argument), Times.Once);
    }

    [Fact]
    public void UnitInstance_Null_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        string? argument = null;
        Mock<ISemanticDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, GenerateSymbol, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithUnitInstance(argument), Times.Once);
    }

    [Fact]
    public void UnitInstance_Object_TryRecordArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, GenerateSymbol, Mock.Of<ISemanticDefaultUnitInstanceRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(Mock.Of<object>());

        Assert.False(outcome);
    }

    private static IParameterSymbol GenerateSymbol { get; } = Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == nameof(DefaultUnitInstanceAttribute.UnitInstance));
}
