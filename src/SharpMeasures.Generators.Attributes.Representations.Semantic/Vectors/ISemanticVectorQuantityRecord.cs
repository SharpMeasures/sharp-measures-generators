namespace SharpMeasures.Generators.Attributes.Vectors;

using Microsoft.CodeAnalysis;

using OneOf;
using OneOf.Types;

/// <summary>Represents a <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface ISemanticVectorQuantityRecord
{
    /// <summary>The <see cref="ITypeSymbol"/> of the unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }

    /// <summary>The dimension of the quantity.</summary>
    public abstract OneOf<None, int> Dimension { get; }
}
