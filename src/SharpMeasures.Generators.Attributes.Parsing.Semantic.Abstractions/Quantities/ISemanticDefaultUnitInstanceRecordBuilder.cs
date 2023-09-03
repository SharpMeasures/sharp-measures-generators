namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Quantities;

/// <summary>Handles incremental construction of <see cref="ISemanticDefaultUnitInstanceRecord"/>.</summary>
public interface ISemanticDefaultUnitInstanceRecordBuilder : IRecordBuilder<ISemanticDefaultUnitInstanceRecord>
{
    /// <summary>Specifies the name of the default unit instance.</summary>
    /// <param name="unitInstance">The name of the default unit instance.</param>
    public abstract void WithUnitInstance(string? unitInstance);

    /// <summary>Specifies the symbol of the default unit instance.</summary>
    /// <param name="symbol">The symbol of the default unit instance.</param>
    public abstract void WithSymbol(string? symbol);
}
