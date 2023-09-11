namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.QuantityOperationMapperCases;

using Moq;

using SharpAttributeParser.Mappers;
using SharpAttributeParser.Mappers.MappedRecorders;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class TryMapNamedParameter_Combined
{
    private static IMappedCombinedNamedArgumentRecorder? Target(ICombinedMapper<IQuantityOperationRecordBuilder> mapper, string parameterName, IQuantityOperationRecordBuilder recordBuilder) => mapper.TryMapNamedParameter(parameterName, recordBuilder);

    private MapperContext Context { get; }

    public TryMapNamedParameter_Combined(IAdaptiveMapperDependencyProvider<IQuantityOperationRecordBuilder, ISemanticQuantityOperationRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, string.Empty, Mock.Of<IQuantityOperationRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void Position_OperationPosition_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = OperationPosition.Left;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, PositionParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithPosition(argument, syntax), Times.Once);
    }

    [Fact]
    public void Position_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(PositionParameterName, Mock.Of<object>());

    [Fact]
    public void MirrorMode_OperationMirrorMode_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = OperationMirrorMode.Adaptive;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, MirrorModeParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithMirrorMode(argument, syntax), Times.Once);
    }

    [Fact]
    public void MirrorMode_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(MirrorModeParameterName, Mock.Of<object>());

    [Fact]
    public void Implementation_OperationImplementation_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = OperationImplementation.StaticMethod;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, ImplementationParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithImplementation(argument, syntax), Times.Once);
    }

    [Fact]
    public void Implementation_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(ImplementationParameterName, Mock.Of<object>());

    [Fact]
    public void MirroredImplementation_OperationImplementation_TryRecordArgumentReturnsTrueAndRecordsArgument()
    {
        var argument = OperationImplementation.StaticMethod;
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, MirroredImplementationParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithMirroredImplementation(argument, syntax), Times.Once);
    }

    [Fact]
    public void MirroredImplementation_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(MirroredImplementationParameterName, Mock.Of<object>());

    [Fact]
    public void MethodName_String_TryRecordArgumentReturnsTrueAndRecordsArgument() => MethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string.Empty);

    [Fact]
    public void MethodName_Null_TryRecordArgumentReturnsTrueAndRecordsArgument() => MethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(null);

    [Fact]
    public void MethodName_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(MethodNameParameterName, Mock.Of<object>());

    [Fact]
    public void StaticMethodName_String_TryRecordArgumentReturnsTrueAndRecordsArgument() => StaticMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string.Empty);

    [Fact]
    public void StaticMethodName_Null_TryRecordArgumentReturnsTrueAndRecordsArgument() => StaticMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(null);

    [Fact]
    public void StaticMethodName_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(StaticMethodNameParameterName, Mock.Of<object>());

    [Fact]
    public void MirroredMethodName_String_TryRecordArgumentReturnsTrueAndRecordsArgument() => MirroredMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string.Empty);

    [Fact]
    public void MirroredMethodName_Null_TryRecordArgumentReturnsTrueAndRecordsArgument() => MirroredMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(null);

    [Fact]
    public void MirroredMethodName_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(MirroredMethodNameParameterName, Mock.Of<object>());

    [Fact]
    public void MirroredStaticMethodName_String_TryRecordArgumentReturnsTrueAndRecordsArgument() => MirroredStaticMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string.Empty);

    [Fact]
    public void MirroredStaticMethodName_Null_TryRecordArgumentReturnsTrueAndRecordsArgument() => MirroredStaticMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(null);

    [Fact]
    public void MirroredStaticMethodName_Object_TryRecordArgumentReturnsFalse() => TryRecordArgumentReturnsFalse(MirroredStaticMethodNameParameterName, Mock.Of<object>());

    [AssertionMethod]
    private void MethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string? argument)
    {
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, MethodNameParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithMethodName(argument, syntax), Times.Once);
    }

    [AssertionMethod]
    private void StaticMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string? argument)
    {
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, StaticMethodNameParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithStaticMethodName(argument, syntax), Times.Once);
    }

    [AssertionMethod]
    private void MirroredMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string? argument)
    {
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, MirroredMethodNameParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithMirroredMethodName(argument, syntax), Times.Once);
    }

    [AssertionMethod]
    private void MirroredStaticMethodName_TryRecordArgumentReturnsTrueAndRecordsArgument(string? argument)
    {
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IQuantityOperationRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, MirroredStaticMethodNameParameterName, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithMirroredStaticMethodName(argument, syntax), Times.Once);
    }

    [AssertionMethod]
    private void TryRecordArgumentReturnsFalse(string parameterName, object? argument)
    {
        var recorder = Target(Context.Mapper, parameterName, Mock.Of<IQuantityOperationRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(argument, ExpressionSyntaxFactory.Create());

        Assert.False(outcome);
    }

    private static string PositionParameterName => nameof(QuantityOperationAttribute<object, object>.Position);
    private static string MirrorModeParameterName => nameof(QuantityOperationAttribute<object, object>.MirrorMode);
    private static string ImplementationParameterName => nameof(QuantityOperationAttribute<object, object>.Implementation);
    private static string MirroredImplementationParameterName => nameof(QuantityOperationAttribute<object, object>.MirroredImplementation);
    private static string MethodNameParameterName => nameof(QuantityOperationAttribute<object, object>.MethodName);
    private static string StaticMethodNameParameterName => nameof(QuantityOperationAttribute<object, object>.StaticMethodName);
    private static string MirroredMethodNameParameterName => nameof(QuantityOperationAttribute<object, object>.MirroredMethodName);
    private static string MirroredStaticMethodNameParameterName => nameof(QuantityOperationAttribute<object, object>.MirroredStaticMethodName);
}
