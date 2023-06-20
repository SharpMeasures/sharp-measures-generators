namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
public interface IQuantitySum
{
    /// <summary>The quantity that represents the sum of two instances of the implementing quantity.</summary>
    public abstract ITypeSymbol Sum { get; }
}
