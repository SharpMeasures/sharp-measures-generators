namespace SharpMeasures.Generators.Attributes.Parsing.QuantitiesCases.SemanticDefaultUnitInstanceRecorderFactoryCases.DefaultUnitInstanceRecordBuilderCases;

using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Xunit;

public sealed class WithUnitInstance
{
    private static void Target(ISemanticDefaultUnitInstanceRecordBuilder recordBuilder, string? unitInstance) => recordBuilder.WithUnitInstance(unitInstance);

    private RecordBuilderContext Context { get; } = RecordBuilderContext.Create();

    [Fact]
    public void String_Recorded() => Recorded(string.Empty);

    [Fact]
    public void Null_Recorded() => Recorded(null);

    [AssertionMethod]
    private void Recorded(string? unitInstance)
    {
        Target(Context.RecordBuilder, unitInstance);

        var actual = Context.RecordBuilder.Build();

        Assert.Equal(unitInstance, actual.UnitInstance);
    }
}
