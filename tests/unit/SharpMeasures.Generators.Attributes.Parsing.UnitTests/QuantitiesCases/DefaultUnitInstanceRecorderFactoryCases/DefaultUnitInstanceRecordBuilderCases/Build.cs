namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using OneOf.Types;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Quantities;

using System;

using Xunit;

public sealed class Build
{
    private static IDefaultUnitInstanceRecord Target(IDefaultUnitInstanceRecordBuilder recordBuilder) => recordBuilder.Build();

    private RecordBuilderContext Context { get; } = RecordBuilderContext.Create();

    [Fact]
    public void MultipleInvokations_InvalidOperationException()
    {
        Context.RecordBuilder.WithUnitInstance(string.Empty, ExpressionSyntaxFactory.Create());

        Target(Context.RecordBuilder);

        var exception = Record.Exception(() => Target(Context.RecordBuilder));

        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void Unmodified_InvalidOperationException()
    {
        var exception = Record.Exception(() => Target(Context.RecordBuilder));

        Assert.IsType<InvalidOperationException>(exception);
    }

    [Fact]
    public void Minimum_RecordHasDefaultValues()
    {
        Context.RecordBuilder.WithUnitInstance(string.Empty, ExpressionSyntaxFactory.Create());

        var actual = Target(Context.RecordBuilder);

        OneOfAssertions.Equal(new None(), actual.Symbol);
        OneOfAssertions.Equal(new None(), actual.Syntactic.Symbol);
    }
}
