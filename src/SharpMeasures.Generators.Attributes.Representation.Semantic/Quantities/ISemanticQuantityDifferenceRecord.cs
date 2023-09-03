namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents the arguments of a <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface ISemanticQuantityDifferenceRecord
{
    /// <summary>The quantity that represents the difference between two instances of the implementing quantity.</summary>
    public abstract ITypeSymbol Difference { get; }
}
