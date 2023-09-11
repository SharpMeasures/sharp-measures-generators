namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceMapperCases;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapNamedParameter_Combined
{
    private static IMappedCombinedNamedArgumentRecorder? Target(ICombinedMapper<IDefaultUnitInstanceRecordBuilder> mapper, string parameterName, IDefaultUnitInstanceRecordBuilder recordBuilder) => mapper.TryMapNamedParameter(parameterName, recordBuilder);

    private MapperContext Context { get; }

    public TryMapNamedParameter_Combined(IAdaptiveMapperDependencyProvider<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, string.Empty, Mock.Of<IDefaultUnitInstanceRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void Symbol_String_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = string.Empty;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, SymbolParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithSymbol(argument, syntax), Times.Once);
    }

    [Fact]
    public void Symbol_Null_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        string? argument = null;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, SymbolParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithSymbol(argument, syntax), Times.Once);
    }

    [Fact]
    public void Symbol_Object_TryRecordArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, SymbolParameterName, Mock.Of<IDefaultUnitInstanceRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(Mock.Of<object>(), ExpressionSyntaxFactory.Create());

        Assert.False(outcome);
    }

    private static string SymbolParameterName => nameof(DefaultUnitInstanceAttribute.Symbol);
}
