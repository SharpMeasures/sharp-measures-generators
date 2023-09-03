namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.DefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;

using Xunit;

public sealed class WithSymbol
{
    private static void Target(IDefaultUnitInstanceRecordBuilder recordBuilder, string? symbol, ExpressionSyntax syntax) => recordBuilder.WithSymbol(symbol, syntax);

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
    private void Recorded(string? symbol)
    {
        var syntax = ExpressionSyntaxFactory.Create();

        Context.RecordBuilder.WithUnitInstance(string.Empty, ExpressionSyntaxFactory.Create());

        Target(Context.RecordBuilder, symbol, syntax);

        var actual = Context.RecordBuilder.Build();

        Assert.Equal(symbol, actual.Symbol);
        OneOfAssertions.Equal(syntax, actual.Syntactic.Symbol);
    }
}
