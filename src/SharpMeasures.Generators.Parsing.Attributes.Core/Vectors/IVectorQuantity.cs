namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface IVectorQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }

    /// <summary>The dimension of the quantity.</summary>
    public abstract int? Dimension { get; }
}
