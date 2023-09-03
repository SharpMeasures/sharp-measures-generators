namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.SemanticDefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class WithSymbol
{
    private static void Target(ISemanticDefaultUnitInstanceRecordBuilder recordBuilder, string? symbol) => recordBuilder.WithSymbol(symbol);

    private RecordBuilderContext Context { get; } = RecordBuilderContext.Create();

    [Fact]
    public void String_Recorded() => Recorded(string.Empty);

    [Fact]
    public void Null_Recorded() => Recorded(null);

    [AssertionMethod]
    private void Recorded(string? symbol)
    {
        Context.RecordBuilder.WithUnitInstance(string.Empty);

        Target(Context.RecordBuilder, symbol);

        var actual = Context.RecordBuilder.Build();

        Assert.Equal(symbol, actual.Symbol);
    }
}
