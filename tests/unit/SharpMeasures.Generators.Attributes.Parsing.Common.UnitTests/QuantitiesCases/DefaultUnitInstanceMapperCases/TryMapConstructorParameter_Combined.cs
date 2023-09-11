﻿namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceMapperCases;

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
    private static IMappedCombinedConstructorArgumentRecorder? Target(ICombinedMapper<IDefaultUnitInstanceRecordBuilder> mapper, IParameterSymbol parameter, IDefaultUnitInstanceRecordBuilder recordBuilder) => mapper.TryMapConstructorParameter(parameter, recordBuilder);

    private MapperContext Context { get; }

    public TryMapConstructorParameter_Combined(IAdaptiveMapperDependencyProvider<IDefaultUnitInstanceRecordBuilder, ISemanticDefaultUnitInstanceRecordBuilder> dependencyProvider)
    {
        Context = MapperContext.Create(dependencyProvider);
    }

    [Fact]
    public void NoMatching_ReturnsNull()
    {
        var recorder = Target(Context.Mapper, Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == string.Empty), Mock.Of<IDefaultUnitInstanceRecordBuilder>());

        Assert.Null(recorder);
    }

    [Fact]
    public void UnitInstance_String_TryRecordArgumentReturnsTrueAndRecordsArgument() => UnitInstance_TryRecordArgumentReturnsTrueAndRecordsArgument(string.Empty);

    [Fact]
    public void UnitInstance_Null_TryRecordArgumentReturnsTrueAndRecordsArgument() => UnitInstance_TryRecordArgumentReturnsTrueAndRecordsArgument(null);

    [Fact]
    public void UnitInstance_String_TryRecordParamsArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, UnitInstanceParameter, Mock.Of<IDefaultUnitInstanceRecordBuilder>());

        var outcome = recorder!.TryRecordParamsArgument(string.Empty, Mock.Of<IReadOnlyList<ExpressionSyntax>>());

        Assert.False(outcome);
    }

    [Fact]
    public void UnitInstance_String_TryRecordDefaultArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, UnitInstanceParameter, Mock.Of<IDefaultUnitInstanceRecordBuilder>());

        var outcome = recorder!.TryRecordDefaultArgument(true);

        Assert.False(outcome);
    }

    [Fact]
    public void UnitInstance_Object_TryRecordArgumentReturnsFalse()
    {
        var recorder = Target(Context.Mapper, UnitInstanceParameter, Mock.Of<IDefaultUnitInstanceRecordBuilder>());

        var outcome = recorder!.TryRecordArgument(Mock.Of<object>(), ExpressionSyntaxFactory.Create());

        Assert.False(outcome);
    }

    [AssertionMethod]
    private void UnitInstance_TryRecordArgumentReturnsTrueAndRecordsArgument(string? argument)
    {
        var syntax = ExpressionSyntaxFactory.Create();
        Mock<IDefaultUnitInstanceRecordBuilder> recordBuilderMock = new();

        var recorder = Target(Context.Mapper, UnitInstanceParameter, recordBuilderMock.Object);

        var outcome = recorder!.TryRecordArgument(argument, syntax);

        Assert.True(outcome);

        recordBuilderMock.Verify((recordBuilder) => recordBuilder.WithUnitInstance(argument, syntax), Times.Once);
    }

    private static IParameterSymbol UnitInstanceParameter { get; } = Mock.Of<IParameterSymbol>(static (symbol) => symbol.Name == nameof(DefaultUnitInstanceAttribute.UnitInstance));
}