namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;

using Xunit;

public sealed class WithUnitInstance
{
    private static void Target(IDefaultUnitInstanceRecordBuilder recordBuilder, string? unitInstance, ExpressionSyntax syntax) => recordBuilder.WithUnitInstance(unitInstance, syntax);

    private RecordBuilderContext Context { get; } = RecordBuilderContext.Create();

    [Fact]
    public void NullSyntax_ArgumentNullException()
    {
        var exception = Record.Exception(() => Target(Context.RecordBuilder, string.Empty, null!));

        Assert.IsType<ArgumentNullException>(exception);
    }

    [Fact]
    public void String_Recorded() => Recorded(string.Empty);

    [Fact]
    public void Null_Recorded() => Recorded(null);

    [AssertionMethod]
    private void Recorded(string? unitInstance)
    {
        var syntax = ExpressionSyntaxFactory.Create();

        Target(Context.RecordBuilder, unitInstance, syntax);

        var actual = Context.RecordBuilder.Build();

        Assert.Equal(unitInstance, actual.UnitInstance);
        Assert.Equal(syntax, actual.Syntactic.UnitInstance);
    }
}
