namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpAttributeParser;

using SharpMeasures.Generators.Attributes.Scalars;

/// <summary>Handles incremental construction of <see cref="ISemanticScalarQuantityRecord"/>.</summary>
public interface ISemanticScalarQuantityRecordBuilder : IRecordBuilder<ISemanticScalarQuantityRecord>
{
    /// <summary>Specifies the unit that describes the quantity.</summary>
    /// <param name="unit">The unit that describes the quantity.</param>
    public abstract void WithUnit(ITypeSymbol unit);

    /// <summary>Specifies whether the quantity is biased.</summary>
    /// <param name="biased">Indicates whether the quantity is biased.</param>
    public abstract void WithBiased(bool biased);
}
