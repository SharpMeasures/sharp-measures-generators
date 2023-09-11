namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceMapperCases;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapNamedParameter_Semantic
{
    private static IMappedSemanticNamedArgumentRecorder? Target(ISemanticMapper<ISemanticDefaultUnitInstanceRecordBuilder> mapper, string parameterName, ISemanticDefaultUnitInstanceRecordBuilder recordBuilder) => mapper.TryMapNamedParameter(parameterName, recordBuilder);

    private MapperContext Context { get; }

    public TryMapNamedParameter_Semantic(IAdaptiveMapperDependencyProvider<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, string.Empty, Mock.Of<ISemanticDefaultUnitInstanceRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void Symbol_String_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = string.Empty;
        Mock<ISemanticDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, SymbolParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithSymbol(argument), Times.Once);
    }

    [Fact]
    public void Symbol_Null_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        string? argument = null;
        Mock<ISemanticDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, SymbolParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithSymbol(argument), Times.Once);
    }

    [Fact]
    public void Symbol_Object_TryRecordArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, SymbolParameterName, Mock.Of<ISemanticDefaultUnitInstanceRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(Mock.Of<object>());

        Assert.False(outcome);
    }

    private static string SymbolParameterName => nameof(DefaultUnitInstanceAttribute.Symbol);
}
