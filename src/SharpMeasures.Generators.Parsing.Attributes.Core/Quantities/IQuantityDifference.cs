namespace SharpMeasures.Generators.Parsing.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface IQuantityDifference
{
    /// <summary>The quantity that represents the difference between two instances of the implementing quantity.</summary>
    public abstract ITypeSymbol Difference { get; }
}
