namespace SharpMeasures.Generators.Parsing.Attributes.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
public interface ISpecializedVectorQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original vector quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }
}
