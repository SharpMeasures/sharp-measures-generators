namespace SharpMeasures.Generators.Attributes.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents the arguments of a <see cref="QuantitySumAttribute{TSum}"/>.</summary>
public interface ISemanticQuantitySumRecord
{
    /// <summary>The quantity that represents the sum of two instances of the implementing quantity.</summary>
    public abstract ITypeSymbol Sum { get; }
}
