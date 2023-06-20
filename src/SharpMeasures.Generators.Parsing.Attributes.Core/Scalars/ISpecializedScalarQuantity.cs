namespace SharpMeasures.Generators.Parsing.Attributes.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="SpecializedScalarQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedScalarQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original scalar quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
